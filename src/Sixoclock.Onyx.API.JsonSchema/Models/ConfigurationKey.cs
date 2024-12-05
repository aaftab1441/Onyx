namespace Sixoclock.Onyx.API.JsonSchema.Models
{
   public  class ConfigurationKey
    {
        private string _key;
        private bool _readonly;
        private string _value;

        [Newtonsoft.Json.JsonProperty("key", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                {
                    _key = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("readonly", Required = Newtonsoft.Json.Required.Always)]
        public bool Readonly
        {
            get { return _readonly; }
            set
            {
                if (_readonly != value)
                {
                    _readonly = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                   
                }
            }
        }
    }
}
