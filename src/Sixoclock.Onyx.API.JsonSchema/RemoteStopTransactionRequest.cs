using Sixoclock.Onyx.API.JsonSchema.Base;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class RemoteStopTransactionRequest : BaseDTO<RemoteStopTransactionRequest>
    {
        private int _transactionId;

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
