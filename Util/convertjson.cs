using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace CASSAWS.Util
{
    public static class convertjson
    {
        public static T Get<T>(string valuekey)
        {
            return valuekey == null ? default(T) : JsonConvert.DeserializeObject<T>(valuekey);
        }
    }
}
