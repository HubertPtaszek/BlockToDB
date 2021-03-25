using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class Editor
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nodes")]
        public Dictionary<string, Node> Nodes { get; set; }
    }
}
