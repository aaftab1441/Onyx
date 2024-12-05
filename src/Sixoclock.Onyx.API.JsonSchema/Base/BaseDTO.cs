using Newtonsoft.Json;

namespace Sixoclock.Onyx.API.JsonSchema.Base
{
    public abstract class BaseDTO<T>
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static T FromJson(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
