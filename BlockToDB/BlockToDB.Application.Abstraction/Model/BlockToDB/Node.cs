using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class Node
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("data")]
        Dictionary<string, string> Data { get; set; }

        [JsonProperty("inputs")]
        Dictionary<string, Input> Inputs { get; set; }

        [JsonProperty("outputs")]
        Dictionary<string, Output> Outputs { get; set; }

        [JsonProperty("position")]
        public decimal[] Position { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
