using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class Output
    {
        [JsonProperty("num")]
        public Num Num { get; set; }

        [JsonProperty("out1")]
        public Num Out1 { get; set; }

        [JsonProperty("out2")]
        public Num Out2 { get; set; }
    }
}
