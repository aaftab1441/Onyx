using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sixoclock.Onyx.API.JsonSchema.Internal;
using Sixoclock.Onyx.API.JsonSchema.Models;
using Sixoclock.Onyx.Configuration;

namespace Sixoclock.Onyx.API.JsonClient
{
    public class DeviceManager: IDeviceManager
    {
        private readonly IConfigurationRoot _appConfiguration;

        public DeviceManager(IAppConfigurationAccessor configurationAccessor)
        {
            _appConfiguration = configurationAccessor.Configuration;
            var connectionString = _appConfiguration.GetConnectionString("MessageBusConnectionString");
            var queueName = _appConfiguration["QueueName"];
            _client = new QueueClient(connectionString, queueName);
        }

        private QueueClient _client;

        public async Task<string> SendMessageToDeviceAsync<T>(string identity, int tenantId, T payload, Dictionary<string, string> metadata = null)
        {
            var messageId = Guid.NewGuid().ToString();
            List<object> messageList=new List<object>();
            messageList.Add(MessageType.CALL);
            messageList.Add(messageId);
            messageList.Add(payload.GetType().Name.Replace("Request",""));
            messageList.Add(payload);
            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageList)))
            {
                ContentType = "application/json",
                Label = identity,
                MessageId = messageId,
                TimeToLive = TimeSpan.FromMinutes(5)
            };
            message.UserProperties.Add("TenantId",tenantId);
            message.UserProperties.Add("Identity",identity);
            if(metadata!=null && metadata.Count>0)
                message.UserProperties.Add(APIMetadata.Metadata,JsonConvert.SerializeObject(metadata));
            await _client.SendAsync(message);
            return messageId;
        }



    }
}