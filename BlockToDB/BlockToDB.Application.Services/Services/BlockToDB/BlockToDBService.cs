using BlockToDB.Data;
using BlockToDB.Domain;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BlockToDB.Application
{
    public class BlockToDBService : ServiceBase, IBlockToDBService
    {
        #region Dependencies

        [Inject]
        public BlockToDBConverter BlockToDBConverter { get; set; }

        [Inject]
        public IDatabaseSchemaRepository DatabaseSchemaRepository { get; set; }

        #endregion Dependencies

        public object GetSchemasToList(DataSourceLoadOptionsBase loadOptions)
        {
            return DatabaseSchemaRepository.GetDatabaseSchemaToList(loadOptions);
        }

        public BlockToDBVM GetBlockToDBVM(int id)
        {
            DatabaseSchema databaseSchema = DatabaseSchemaRepository.GetSingle(x => x.Id == id);
            BlockToDBVM model = BlockToDBConverter.ToBlockToDBVM(databaseSchema);
            return model;
        }

        public int GenerateScript(BlockToDBGenerateVM model)
        {
            Editor editor = JsonConvert.DeserializeObject<Editor>(model.Json); //deserializowanie JSON-a
            string result = BlockToDBConverter.ToSqlCode(editor, model.Name); //algorytm do tworzenia skryptu
            DatabaseSchema databaseSchema = new DatabaseSchema();
            if (model.Id.HasValue)
            {
                databaseSchema = DatabaseSchemaRepository.GetSingle(x => x.Id == model.Id.Value);
                BlockToDBConverter.FromBlockToDBGenerateVM(model, databaseSchema, result);
                DatabaseSchemaRepository.Edit(databaseSchema);
            }
            else
            {
                databaseSchema = new DatabaseSchema()
                {
                    Json = model.Json,
                    Script = result,
                    Name = model.Name
                };
                DatabaseSchemaRepository.Add(databaseSchema);
            }
            DatabaseSchemaRepository.Save();
            return databaseSchema.Id;
        }

        public string CreateRemoteDataBase(BlockToDBGenerateRemoteVM model)
        {
            BlockToDBGenerateVM blockToDBGenerate = BlockToDBConverter.FromBlockToDBGenerateRemoteVM(model);
            int fileId = GenerateScript(blockToDBGenerate);
            DatabaseSchema databaseSchema = DatabaseSchemaRepository.GetSingle(x => x.Id == fileId);
            string connectionString = string.Format("Data Source={0};Initial Catalog=master;Integrated Security=False; User Id={1};Password={2}", model.Url, model.UserName, model.Password);
            Editor editor = JsonConvert.DeserializeObject<Editor>(model.Json);
            List<string> commands = BlockToDBConverter.ToSqlCodeList(editor, model.Name);
            string result = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(commands[0], conn);
                    cmd.ExecuteNonQuery();
                    conn.ChangeDatabase(model.Name);
                    cmd.CommandText = commands[1];
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                result = "Error";
            }
            return result;
        }

        public int AddOrEdit(BlockToDBAddOrEditVM model)
        {
            DatabaseSchema databaseSchema = new DatabaseSchema();
            if (model.Id.HasValue)
            {
                databaseSchema = DatabaseSchemaRepository.GetSingle(x => x.Id == model.Id.Value);
            }
            databaseSchema = BlockToDBConverter.FromBlockToDBAddOrEditVM(model, databaseSchema);
            DatabaseSchemaRepository.AddOrEdit(databaseSchema);
            DatabaseSchemaRepository.Save();
            return databaseSchema.Id;
        }

        public void Delete(int id)
        {
            DatabaseSchema databaseSchema = DatabaseSchemaRepository.GetSingle(x => x.Id == id);
            DatabaseSchemaRepository.Delete(databaseSchema);
            DatabaseSchemaRepository.Save();
        }

        public DownloadVM DownloadFile(int id, bool isScript)
        {
            DatabaseSchema databaseSchema = DatabaseSchemaRepository.GetSingle(x => x.Id == id);
            DownloadVM model = BlockToDBConverter.ToDownloadVM(databaseSchema, isScript);
            return model;
        }
    }
}