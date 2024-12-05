using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sixoclock.Onyx.API.Json.WebSockets;
using Sixoclock.Onyx.API.JsonSchema.Internal;
using Sixoclock.Onyx.Configuration;

namespace Sixoclock.Onyx.API.Json.MessageBus
{
    public class OnyxMessageBusManager
    {
        private readonly IConfigurationRoot _appConfiguration;
        private IWebSocketOcppFramework __OCPPFrameworkManager;
        private OnyxWebSocketManager _webSocketManager;
        private QueueClient _client;
        private readonly ILogger _logger;

        public OnyxMessageBusManager(IAppConfigurationAccessor configurationAccessor,
            IWebSocketOcppFramework ocppFrameworkManager, OnyxWebSocketManager webSocketManager, ILogger logger)
        {
            __OCPPFrameworkManager = ocppFrameworkManager;
            _webSocketManager = webSocketManager;
            _logger = logger;
            _appConfiguration = configurationAccessor.Configuration;
            var connectionString = _appConfiguration.GetConnectionString("MessageBusConnectionString");
            var queueName = _appConfiguration["QueueName"];
            _client = new QueueClient(connectionString, queueName,ReceiveMode.ReceiveAndDelete);
            
            _client.RegisterMessageHandler(async (message, token) =>
                {
                    try
                    {
                        string jsonPayload = Encoding.UTF8.GetString(message.Body);
                        int tenantId = Int32.Parse(message.UserProperties["TenantId"].ToString());
                        string identity = message.UserProperties["Identity"].ToString();
                        string connectionId = _webSocketManager.GetIdByTenantAndIdentity(identity, tenantId);
                       Dictionary<string, string> metadata = message.UserProperties.ContainsKey(APIMetadata.Metadata)
                            ? JsonConvert.DeserializeObject<Dictionary<string, string>>(message
                                .UserProperties[APIMetadata.Metadata].ToString())
                            : null;
                        _logger.DebugFormat("Message from MessageBuss Identity: {0} , Tenant:{1}\n Connection: {2} Message:{3} \n Metadata: {4}",
                            identity, tenantId,connectionId,jsonPayload, metadata!=null?JsonConvert.SerializeObject(metadata):"");
                        await __OCPPFrameworkManager.SendMessageAsync(jsonPayload, connectionId, metadata);
                        if (string.IsNullOrEmpty(connectionId))
                        {
                            foreach (var onyxWebSocket in _webSocketManager.GetAll())
                            {
                                _logger.DebugFormat("Connection: Identity: {0}, Tenant:{1}",onyxWebSocket.Value.Identity,onyxWebSocket.Value.TenantId);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.DebugFormat("New incomming Message from MessageBuss= failed: {0}", e.ToString());
                    }
                },
                async (e) =>
                {
                    _logger.DebugFormat("New incomming Message from MessageBuss= failed: {0}", e.Exception.ToString());
                    await Task.FromResult<string>(null);
                });
        }
    }
}