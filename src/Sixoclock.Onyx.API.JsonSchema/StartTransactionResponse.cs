using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class StartTransactionResponse:BaseDTO<StartTransactionResponse>
    {
        private IdTagInfo _idTagInfo = new IdTagInfo();
        private int _transactionId;

        [Newtonsoft.Json.JsonProperty("idTagInfo", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public IdTagInfo IdTagInfo
        {
            get { return _idTagInfo; }
            set
            {
                if (_idTagInfo != value)
                {
                    _idTagInfo = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("transactionId", Required = Newtonsoft.Json.Required.Always)]
        public int TransactionId
        {
            get { return _transactionId; }
            set
            {
                if (_transactionId != value)
                {
                    _transactionId = value;
                  
                }
            }
        }
    }
}
