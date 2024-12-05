using System.Collections.Generic;
using Newtonsoft.Json;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.Json.Utility
{
    public static class OCPPGenericHandler
    {
        public static string GetCallResponse(string uniqueId, object payLoad)
        {
            List<object> response = new List<object>();
            response.Add((int) MessageType.CALLRESULT);
            response.Add(uniqueId);
            response.Add(payLoad);
            return JsonConvert.SerializeObject(response);
        }
        //public static string GetCallResponse(string uniqueId, object payLoad)
        //{
        //    List<object> response = new List<object>();
        //    response.Add((int)MessageType.CALLRESULT);
        //    response.Add(uniqueId);
        //    response.Add(payLoad);
        //    return JsonConvert.SerializeObject(response);
        //}
    }
}
