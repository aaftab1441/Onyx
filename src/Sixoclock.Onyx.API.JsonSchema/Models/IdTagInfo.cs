namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public partial class IdTagInfo 
    {
        private StatusEnum _status;
        private string _expiryDate;
        private string _parentIdTag;

        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public StatusEnum Status
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

        [Newtonsoft.Json.JsonProperty("expiryDate", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ExpiryDate
        {
            get { return _expiryDate; }
            set
            {
                if (_expiryDate != value)
                {
                    _expiryDate = value;
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("parentIdTag", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string ParentIdTag
        {
            get { return _parentIdTag; }
            set
            {
                if (_parentIdTag != value)
                {
                    _parentIdTag = value;
                }
            }
        }

    }
}
