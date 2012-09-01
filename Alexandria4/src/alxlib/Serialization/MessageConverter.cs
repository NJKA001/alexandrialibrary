using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace Gnosis.Alexandria.Serialization
{
    public class MessageConverter
    {
        public string SeralizeToJson(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T DeserializeFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
