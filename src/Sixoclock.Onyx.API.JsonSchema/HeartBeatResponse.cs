using Sixoclock.Onyx.API.JsonSchema.Base;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class HeartBeatResponse:BaseDTO<HeartBeatResponse>
    {
        private string _currentTime;

        [Newtonsoft.Json.JsonProperty("currentTime", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
              
                if (_currentTime != value)
                {
                    _currentTime = value;
                   
                }
            }
        }
    }
}
