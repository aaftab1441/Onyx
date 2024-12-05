using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Newtonsoft.Json.Linq;
using Sixoclock.Onyx.API.Json.RequestHandlers;
using Sixoclock.Onyx.API.Json.Utility;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.Json.WebSockets
{
    public class WebSocketOcppFramework : IWebSocketOcppFramework
    {
        
        private readonly Dictionary<string, System.Reflection.TypeInfo> _handlers;
        private readonly List<RPCMessage> _inCommingMessages;
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        private readonly List<RPCMessage> _outGoingMessages;

        public WebSocketOcppFramework(OnyxWebSocketManager sockManager, IServiceProvider serviceProvider, ILogger logger)
        {
            _inCommingMessages = new List<RPCMessage>();
            _outGoingMessages = new List<RPCMessage>();
            _serviceProvider = serviceProvider;
            _logger = logger;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            _handlers = new Dictionary<string, System.Reflection.TypeInfo>();
            foreach (System.Reflection.TypeInfo ti in assembly.DefinedTypes)
            {
                if (ti.ImplementedInterfaces.Contains(typeof(IRequestHandler)))
                {
                    _handlers.Add(ti.GetAttributeValue((OCPPAction dna) =>dna.Name.ToLower()+ Enum.GetName(typeof(RequestHandlerType),dna.Type)), ti);
                }
            }
           // _timer = new Timer(TimerCallback,null,0,30);
           
           

        }
        private void TimerCallback(object state)
        {
            for (int i = 0; i < _inCommingMessages.Count; i++)
            {
                if (_inCommingMessages[i].IsProcessed || (_inCommingMessages[i].ExpiryTime.HasValue && _inCommingMessages[i].ExpiryTime <= DateTime.UtcNow))
                    _inCommingMessages.RemoveAt(i);
            }
        }
        public async Task ProcessMessageAsync(string message, string connectionId)
        {
            try
            {
                var dto = JArray.Parse(message);
                MessageType messageId = (MessageType)Convert.ToInt32(dto[0].ToString());
                string uniqueId = string.Empty;
                string action = string.Empty;
                string payload = string.Empty;
                DateTime expiryTime;
                RPCMessage rpcMessage;
                TypeInfo requestHandler;
                _logger.DebugFormat(messageId == MessageType.CALL ? "Request in OCPPFramework:{0}" : "Response in OcppFramework:{0}", message);
                switch (messageId)
                {
                    case MessageType.CALL:
                        //if (_inCommingMessages.Count > 0)
                        //    return; 
                        uniqueId = dto[1].ToString();
                        action = dto[2].ToString();
                        payload = dto[3].ToString();
                        expiryTime = DateTime.Now.AddMinutes(2);
                        rpcMessage = new RPCMessage() { ConnectionId = connectionId, ExpiryTime = expiryTime, MessageId = messageId, UniqueId = uniqueId, PayLoad = payload, Action = action };

                        _inCommingMessages.Add(rpcMessage);
                        string handlerName = action.ToLower() +
                                             Enum.GetName(typeof(RequestHandlerType), RequestHandlerType.CPTOCS);
                        if (!_handlers.ContainsKey(handlerName))
                            _logger.Debug($"Unable to find Request handler for: {handlerName}");
                        else
                        {
                            requestHandler = this._handlers[handlerName];
                            // ReSharper disable once PossibleNullReferenceException
                            await (_serviceProvider.CreateInstance(requestHandler) as IRequestHandler)
                                ?.HandleRequestAsync(rpcMessage);
                            _inCommingMessages.Remove(rpcMessage);
                        }
                        break;
                    case MessageType.CALLRESULT:
                    case MessageType.CALLERROR:
                        uniqueId = dto[1].ToString();
                        var requestMessage = _outGoingMessages.FirstOrDefault(x => x.UniqueId == uniqueId);
                        action = requestMessage?.Action;
                        payload = dto[2].ToString();
                        expiryTime = DateTime.Now.AddMinutes(2);
                        rpcMessage = new RPCMessage() { ConnectionId = connectionId, ExpiryTime = expiryTime, MessageId = messageId, UniqueId = uniqueId, PayLoad = payload, Action = action, Metadata = requestMessage?.Metadata };

                        _inCommingMessages.Add(rpcMessage);
                        string responseHandlerName = action?.ToLower() +
                                             Enum.GetName(typeof(RequestHandlerType), RequestHandlerType.CPTOCS);
                        if (!_handlers.ContainsKey(responseHandlerName))
                            _logger.Debug($"Unable to find Request handler for: {responseHandlerName}");
                        else
                        {
                            requestHandler = this._handlers[responseHandlerName];
                            // ReSharper disable once PossibleNullReferenceException
                            await (_serviceProvider.CreateInstance(requestHandler) as IRequestHandler)?.HandleRequestAsync(
                                rpcMessage);
                            _inCommingMessages.Remove(rpcMessage);
                            _outGoingMessages.Remove(requestMessage);
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
               _logger.Debug(e.Message,e);
            }
           


        }

        public async Task SendMessageAsync(string message, string connectionId, Dictionary<string, string> metadata)
        {
            _logger.Debug("ReceivedMessage in OCPPFramework:ConnectionId: "+connectionId);
            var dto = JArray.Parse(message);
            MessageType messageId = (MessageType)Convert.ToInt32(dto[0].ToString());

                    //if (_outGoingMessages.Count > 0)
                    //    return;
                    string uniqueId = dto[1].ToString();
                    string action = dto[2].ToString();
                    string payload = dto[3].ToString();
                    DateTime expiryTime = DateTime.Now.AddMinutes(2);
                    var rpcMessage = new RPCMessage() { ConnectionId = connectionId, ExpiryTime = expiryTime, MessageId = messageId, UniqueId = uniqueId, PayLoad = payload, Action = action, Metadata = metadata};

                    _outGoingMessages.Add(rpcMessage);
                    var requestHandler = this._handlers[action.ToLower() + Enum.GetName(typeof(RequestHandlerType), RequestHandlerType.CSTOCP)];
                    if (requestHandler != null)
                      // ReSharper disable once PossibleNullReferenceException
                     await (_serviceProvider.CreateInstance(requestHandler) as IRequestHandler)?.HandleRequestAsync(rpcMessage);
           

        }
    }
}
