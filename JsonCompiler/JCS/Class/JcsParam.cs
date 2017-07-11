using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonCS.JCS.Class
{
    public class JcsParam
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("type")]
        public string Type;


    }
}
