using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JsonCS.JCS.Class
{
    public class JcsMethod
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("access")]
        public string Access { get; set; }
        [JsonProperty("returns")]
        public string ReturnType { get; set; }
        [JsonProperty("params")]
        public JcsParam[] Params { get; set; }
        [JsonProperty("il")]
        public byte[] IL { get; set; }


        public MethodAttributes AccessAttribute()
        {
            switch (Access ?? "")
            {
            case "public":
            return MethodAttributes.Public;
            case "private":
            return MethodAttributes.Private;
            default:
            return MethodAttributes.Public;
            }
        }
    }
}
