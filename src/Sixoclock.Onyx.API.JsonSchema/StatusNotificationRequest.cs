using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public partial class StatusNotificationRequest :BaseDTO<StatusNotificationRequest>
    {
        private int _connectorId;
        private StatusNotificationRequestErrorCode _errorCode;
        private string _info;
        private StatusNotificationRequestStatus _status;
        private System.DateTime? _timestamp;
        private string _vendorId;
        private string _vendorErrorCode;

       
        public int ConnectorId
        {
            get { return _connectorId; }
            set
            {
                if (_connectorId != value)
                {
                    _connectorId = value;
                   
                }
            }
        }

      
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public StatusNotificationRequestErrorCode ErrorCode
        {
            get { return _errorCode; }
            set
            {
                if (_errorCode != value)
                {
                    _errorCode = value;
                  
                }
            }
        }

      
        public string Info
        {
            get { return _info; }
            set
            {
                if (_info != value)
                {
                    _info = value;
                  
                }
            }
        }

     
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public StatusNotificationRequestStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                  
                }
            }
        }

      
        public System.DateTime? Timestamp
        {
            get { return _timestamp; }
            set
            {
                if (_timestamp != value)
                {
                    _timestamp = value;
                
                }
            }
        }

     
        public string VendorId
        {
            get { return _vendorId; }
            set
            {
                if (_vendorId != value)
                {
                    _vendorId = value;
                 
                }
            }
        }

     
        public string VendorErrorCode
        {
            get { return _vendorErrorCode; }
            set
            {
                if (_vendorErrorCode != value)
                {
                    _vendorErrorCode = value;
                 
                }
            }
        }

     

      

      
    }
}
