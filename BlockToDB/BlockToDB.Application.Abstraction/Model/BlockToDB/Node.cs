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
        public Dictionary<string, string> Data { get; set; }

        [JsonProperty("inputs")]
        public Dictionary<string, Input> Inputs { get; set; }

        [JsonProperty("outputs")]
        public Dictionary<string, Output> Outputs { get; set; }

        [JsonProperty("position")]
        public decimal[] Position { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public string GetTableName()
        {
            return Data.FirstOrDefault(x => x.Key == "tableName").Value;
        }
        public string GetTableField(int id, ref List<string> primaryKeys)
        {
            StringBuilder field = new StringBuilder();
            string fieldName = Data.FirstOrDefault(x => x.Key == id + "-name").Value;
            string fieldType = Data.FirstOrDefault(x => x.Key == id + "-type").Value;
            string isPrimaryKey = Data.FirstOrDefault(x => x.Key == id + "-isPrimaryKey").Value;
            string notNull = Data.FirstOrDefault(x => x.Key == id + "-notNull").Value;
            string unique = Data.FirstOrDefault(x => x.Key == id + "-unique").Value;
            if(fieldType == "CHARACTER")
            {
                field.Append(fieldName + " " + fieldType + "(255)");

            }
            else
            {
                field.Append(fieldName + " " + fieldType);

            }

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
        public void GetInheritTableConnection(string nodeToName, ref List<InheritConn> incheritConns, Editor model)
        {
            Dictionary<string, Output> inputs = GetConnectionsOutputs();
            foreach (KeyValuePair<string, Output> input in inputs)
            {
                foreach (Connection connection in input.Value.Connections)
                {
                    incheritConns.Add(new InheritConn()
                    {
                        NodeFrom = model.GetTableName(connection.Node),
                        NodeTo = nodeToName,
                        Field = GetFieldName(int.Parse(input.Key)),
                        Field2 = model.GetTableField(connection.Node, int.Parse(connection.Input))
                    });
                }
            }
        }

        public string GetTableOnlyPKField(int id, ref List<string> primaryKeys, string nodeToName, ref List<InheritConn> incheritConns)
        {
            StringBuilder field = new StringBuilder();
            string isPrimaryKey = Data.FirstOrDefault(x => x.Key == id + "-isPrimaryKey").Value;

            if (isPrimaryKey == "true")
            {
                string fieldName = Data.FirstOrDefault(x => x.Key == id + "-name").Value;
                string fieldType = Data.FirstOrDefault(x => x.Key == id + "-type").Value;
                string notNull = Data.FirstOrDefault(x => x.Key == id + "-notNull").Value;
                string unique = Data.FirstOrDefault(x => x.Key == id + "-unique").Value;

                primaryKeys.Add(fieldName);
                if (fieldType == "CHARACTER")
                {
                    field.Append(fieldName + " " + fieldType + "(255)");

                }
                else
                {
                    field.Append(fieldName + " " + fieldType);

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
                incheritConns.Add(new InheritConn()
                {
                    NodeFrom = GetTableName(),
                    NodeTo = nodeToName,
                    Field = fieldName
                });
                ;

            }
            return field.ToString();
        }
        public string GetTableNonePKField(int id)
        {
            StringBuilder field = new StringBuilder();
            string isPrimaryKey = Data.FirstOrDefault(x => x.Key == id + "-isPrimaryKey").Value;

            if (isPrimaryKey != "true")
            {
                string fieldName = Data.FirstOrDefault(x => x.Key == id + "-name").Value;
                string fieldType = Data.FirstOrDefault(x => x.Key == id + "-type").Value;
                string notNull = Data.FirstOrDefault(x => x.Key == id + "-notNull").Value;
                string unique = Data.FirstOrDefault(x => x.Key == id + "-unique").Value;

                if (fieldType == "CHARACTER")
                {
                    field.Append(fieldName + " " + fieldType + "(255)");

                }
                else
                {
                    field.Append(fieldName + " " + fieldType);

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
            }
            
            return field.ToString();
        }
        public Dictionary<string, Input> GetConnections() 
        {
            return Inputs.Where(x => x.Key != "inheritFrom").ToDictionary(x => x.Key, x => x.Value);
        }
        public Dictionary<string, Output> GetConnectionsOutputs()
        {
            return Outputs.Where(x => x.Key != "inheritTo").ToDictionary(x => x.Key, x => x.Value);
        }
        public Input GetInheritFrom()
        {
            return Inputs.FirstOrDefault(x => x.Key == "inheritFrom").Value;
        }
        public string GetInheritType()
        {
            return Data.FirstOrDefault(x => x.Key == "inherit-type").Value;
        }
        public Output GetInheritTo()
        {
            return Outputs.FirstOrDefault(x => x.Key == "inheritTo").Value;
        }
        public string GetFieldName(int id)
        {
            string fieldName = Data.FirstOrDefault(x => x.Key == id + "-name").Value;
            return fieldName;
        }
    }
}
