using BlockToDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockToDB.Application
{
    public class BlockToDBConverter : ConverterBase
    {
        public DownloadVM ToDownloadVM(DatabaseSchema databaseSchema, bool isScript)
        {
            DownloadVM model = new DownloadVM()
            {
                Name = isScript ? databaseSchema.Name + ".sql" : databaseSchema.Name + ".json",
                Content = isScript ? databaseSchema.Script : databaseSchema.Json,
                ContentType = isScript ? "text/plain" : "application/json"
            };
            return model;
        }

        public BlockToDBGenerateVM FromBlockToDBGenerateRemoteVM(BlockToDBGenerateRemoteVM model)
        {
            BlockToDBGenerateVM result = new BlockToDBGenerateVM()
            {
                Id = model.Id,
                Json = model.Json
            };
            return result;
        }

        public BlockToDBVM ToBlockToDBVM(DatabaseSchema databaseSchema)
        {
            BlockToDBVM model = new BlockToDBVM() { 
                Id = databaseSchema.Id,
                Json = databaseSchema.Json,
                Name = databaseSchema.Name
            };
            return model;
        }

        public DatabaseSchema FromBlockToDBEditVM(BlockToDBEditVM model, DatabaseSchema databaseSchema)
        {
            databaseSchema.Json = model.Json;
            databaseSchema.Name = model.Name;
            return databaseSchema;
        }

        public DatabaseSchema FromBlockToDBAddVM(BlockToDBAddVM model)
        {
            return new DatabaseSchema()
            {
                Json = model.Json,
                Script = "",
                Name = model.Name
            };
        }

        public DatabaseSchema FromBlockToDBGenerateVM(BlockToDBGenerateVM model, DatabaseSchema databaseSchema, string result)
        {
            databaseSchema.Json = model.Json;
            databaseSchema.Script = result;
            return databaseSchema;
        }

        public string ToSqlCode(Editor model)
        {
            StringBuilder content = new StringBuilder();
            List<InheritConn> inheritConns = new List<InheritConn>();
            foreach (KeyValuePair<string, Node> node in model.Nodes)
            {
                int fieldsCount = node.Value.Inputs.Count - 1;
                StringBuilder nodeContent = new StringBuilder();
                List<string> primaryKeys = new List<string>();
                nodeContent.AppendLine("CREATE TABLE " + node.Value.GetTableName() + " (");
                Input inhetitFrom = node.Value.GetInheritFrom();
                string inhetitType = "0";
                if(inhetitFrom.Connections.Count > 0)
                {
                    inhetitType = node.Value.GetInheritType();
                }
                if (inhetitType == "TPT")
                {
                    Connection conn = inhetitFrom.Connections[0];
                    int fieldsCountInherit = model.Nodes.FirstOrDefault(x => x.Key == conn.Node.ToString()).Value.Inputs.Count - 1;
                    Node inheritNode = model.Nodes.FirstOrDefault(x => x.Key == conn.Node.ToString()).Value;
                    for (int i = 1; i <= fieldsCountInherit; i++)
                    {
                        string fieldtoadd = inheritNode.GetTableOnlyPKField(i, ref primaryKeys, node.Value.GetTableName(), ref inheritConns);
                        if (!string.IsNullOrEmpty(fieldtoadd))
                        {
                            nodeContent.AppendLine(fieldtoadd);

                        }
                    }
                    for (int i = 1; i <= fieldsCount; i++)
                    {
                        string fieldtoadd = node.Value.GetTableNonePKField(i);
                        if (!string.IsNullOrEmpty(fieldtoadd)){
                            nodeContent.AppendLine(fieldtoadd);

                        }
                    }
                    if (primaryKeys.Count != 0)
                    {
                        StringBuilder primaryKeysString = new StringBuilder();

                        primaryKeysString.Append("PRIMARY KEY (");
                        int last = primaryKeys.Count;
                        int j = primaryKeys.Count;
                        foreach (string key in primaryKeys)
                        {
                            primaryKeysString.Append(key);
                            if (last == j)
                            {
                                primaryKeysString.Append(")");
                            }
                            else
                            {
                                j++;
                                primaryKeysString.Append(",");
                            }
                        }
                        nodeContent.AppendLine(primaryKeysString.ToString());
                    }

                }
                else if (inhetitType == "TPH")
                {
                   
                }
                else if (inhetitType == "TPC")
                {
                    Connection conn = inhetitFrom.Connections[0];
                    int fieldsCountInherit = model.Nodes.FirstOrDefault(x => x.Key == conn.Node.ToString()).Value.Inputs.Count - 1;
                    Node inheritNode = model.Nodes.FirstOrDefault(x => x.Key == conn.Node.ToString()).Value;
                    for (int i = 1; i <= fieldsCountInherit; i++)
                    {
                        nodeContent.AppendLine(inheritNode.GetTableField(i, ref primaryKeys));
                    }
                    for (int i = 1; i <= fieldsCount; i++)
                    {
                        nodeContent.AppendLine(node.Value.GetTableField(i, ref primaryKeys));
                    }
                    if (primaryKeys.Count != 0)
                    {
                        StringBuilder primaryKeysString = new StringBuilder();

                        primaryKeysString.Append("PRIMARY KEY (");
                        int last = primaryKeys.Count;
                        int j = primaryKeys.Count;
                        foreach (string key in primaryKeys)
                        {
                            primaryKeysString.Append(key);
                            if (last == j)
                            {
                                primaryKeysString.Append(")");
                            }
                            else
                            {
                                j++;
                                primaryKeysString.Append(",");
                            }
                        }
                        nodeContent.AppendLine(primaryKeysString.ToString());
                    }
                }
                else
                {
                    for (int i = 1; i <= fieldsCount; i++)
                    {
                        nodeContent.AppendLine(node.Value.GetTableField(i, ref primaryKeys));
                    }
                    if (primaryKeys.Count != 0)
                    {
                        StringBuilder primaryKeysString = new StringBuilder();

                        primaryKeysString.Append("PRIMARY KEY (");
                        int last = primaryKeys.Count;
                        int j = primaryKeys.Count;
                        foreach (string key in primaryKeys)
                        {
                            primaryKeysString.Append(key);
                            if (last == j)
                            {
                                primaryKeysString.Append(")");
                            }
                            else
                            {
                                j++;
                                primaryKeysString.Append(",");
                            }
                        }
                        nodeContent.AppendLine(primaryKeysString.ToString());
                    }
                }


                nodeContent.AppendLine(");");

                content.AppendLine(nodeContent.ToString());
            }
            Random rnd = new Random();
            foreach (KeyValuePair<string, Node> node in model.Nodes)
            {

                StringBuilder nodeContent = new StringBuilder();
                Dictionary<string, Input> inputs = node.Value.GetConnections();
                string nodeName = node.Value.GetTableName();
                foreach (KeyValuePair<string, Input> input in inputs)
                {
                    foreach (Connection connection in input.Value.Connections)
                    {
                        StringBuilder connContent = new StringBuilder();
                        connContent.Append("ALTER TABLE ");
                        connContent.Append(nodeName);
                        connContent.Append("  ADD CONSTRAINT ");
                        connContent.Append("  FK_");
                        connContent.Append(input.Key);
                        connContent.Append("_");
                        connContent.Append(connection.Output);
                        connContent.Append("_");
                        connContent.Append(rnd.Next(100));
                        connContent.Append(" FOREIGN KEY(");
                        connContent.Append(node.Value.GetFieldName(int.Parse(input.Key)));
                        connContent.Append(") REFERENCES ");
                        connContent.Append(model.GetTableName(connection.Node));
                        connContent.Append("(");
                        connContent.Append(model.GetTableField(connection.Node, int.Parse(connection.Output)));
                        connContent.Append(");");
                        nodeContent.Append(connContent.ToString());
                    }
                }
            }
                foreach (InheritConn inheritConn in inheritConns)
                {

                    StringBuilder connContent = new StringBuilder();
                    connContent.Append("ALTER TABLE ");
                    connContent.Append(inheritConn.NodeTo);
                    connContent.Append("  ADD CONSTRAINT ");
                    connContent.Append("  FK_");
                    connContent.Append(inheritConn.NodeFrom);
                    connContent.Append("_");
                    connContent.Append(inheritConn.NodeTo);
                    connContent.Append("_");
                    connContent.Append(rnd.Next(100));
                    connContent.Append(" FOREIGN KEY(");
                    connContent.Append(inheritConn.Field);
                    connContent.Append(") REFERENCES ");
                    connContent.Append(inheritConn.NodeFrom);
                    connContent.Append("(");
                    connContent.Append(inheritConn.Field);
                    connContent.Append(");");
                    


                    content.AppendLine(connContent.ToString());
                }
                //ALTER TABLE MailSent ADD CONSTRAINT fk_profile_sender_id FOREIGN KEY(profil_sender_id) REFERENCES TABLE-NAME(id);



                return content.ToString();
        }

    }
}