using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class Input
    {
        [JsonProperty("connections")]
        List<Connection> Connections { get; set; }
    }
}
