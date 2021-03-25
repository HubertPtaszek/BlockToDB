using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class Data
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
