using Newtonsoft.Json;
using System.Reflection;

namespace JsonCS.JCS.Class
{
    public class JcsField
    {
        [JsonProperty("name")]
        public string FieldName;
        [JsonProperty("access")]
        public string FieldAccess;
        [JsonProperty("type")]
        public string FieldType;

        public FieldAttributes AccessAttribute()
        {
            switch (FieldAccess ?? "")
            {
            case "public":
            return FieldAttributes.Public;
            case "private":
            return FieldAttributes.Private;
            default:
            return FieldAttributes.Public;
            }
        }
    }
}