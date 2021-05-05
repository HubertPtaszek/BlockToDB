using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class Connection
    {
        [JsonProperty("node")]
        public int Node { get; set; }
        public string Input { get; set; }
    }
}
