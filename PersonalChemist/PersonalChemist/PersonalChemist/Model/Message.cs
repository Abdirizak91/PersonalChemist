using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PersonalChemist.Model
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text
        {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("ConnectionID")]
        public string ConnectionID
        {
            get;
            set;
        }
    }
}
