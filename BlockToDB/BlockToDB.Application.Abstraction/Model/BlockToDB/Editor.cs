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
        public string GetRelationType(int nodeId, int fieldId)
        {
            string fieldName = Nodes.FirstOrDefault(x => x.Key == nodeId.ToString()).Value.Data.FirstOrDefault(x => x.Key == fieldId + "-relation").Value;
            return fieldName;
        }
        public string GetTableField(int id, int nodeId,ref List<string> primaryKeys)
        {
            StringBuilder field = new StringBuilder();
            string fieldName = Nodes.FirstOrDefault(x => x.Key == nodeId.ToString()).Value.Data.FirstOrDefault(x => x.Key == id + "-name").Value;
            string fieldType = Nodes.FirstOrDefault(x => x.Key == nodeId.ToString()).Value.Data.FirstOrDefault(x => x.Key == id + "-type").Value;
            string isPrimaryKey = Nodes.FirstOrDefault(x => x.Key == nodeId.ToString()).Value.Data.FirstOrDefault(x => x.Key == id + "-isPrimaryKey").Value;
            string notNull = Nodes.FirstOrDefault(x => x.Key == nodeId.ToString()).Value.Data.FirstOrDefault(x => x.Key == id + "-notNull").Value;
            string unique = Nodes.FirstOrDefault(x => x.Key == nodeId.ToString()).Value.Data.FirstOrDefault(x => x.Key == id + "-unique").Value;
            field.Append(fieldName + " " + fieldType);

            if (isPrimaryKey == "true")
            {
                primaryKeys.Add(fieldName);
            }
            if (notNull == "true")
            {
                field.Append(" NOT NULL");
            }
            if (unique == "true")
            {
                field.Append(" UNIQUE");
            }
            field.Append(",");
            return field.ToString();
        }

    }
}
