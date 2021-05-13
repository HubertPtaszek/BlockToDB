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

        public string GetTableName(int id)
        {
            return Nodes.FirstOrDefault(x => x.Key == id.ToString()).Value.Data.FirstOrDefault(x => x.Key == "tableName").Value;
        }
        public string GetTableField(int nodeId, int fieldId)
        {
            string fieldName = Nodes.FirstOrDefault(x => x.Key == nodeId.ToString()).Value.Data.FirstOrDefault(x => x.Key == fieldId + "-name").Value;
            return fieldName;
        }
    }
}
