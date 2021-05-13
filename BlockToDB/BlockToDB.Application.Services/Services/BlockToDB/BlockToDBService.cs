using BlockToDB.Data;
using BlockToDB.Domain;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using Ninject;

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
            string result = BlockToDBConverter.ToSqlCode(editor); //algorytm do tworzenia skryptu
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
                    Name = "Script1"
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
            //ToDo remote create
            string result = "";
            return result;
        }

        public int Add(BlockToDBAddVM model)
        {
            DatabaseSchema databaseSchema = BlockToDBConverter.FromBlockToDBAddVM(model);
            DatabaseSchemaRepository.Add(databaseSchema);
            DatabaseSchemaRepository.Save();
            return databaseSchema.Id;
        }

        public void Edit(BlockToDBEditVM model)
        {
            DatabaseSchema databaseSchema = DatabaseSchemaRepository.GetSingle(x => x.Id == model.Id);
            if (databaseSchema == null)
            {
                throw new BussinesException(1001, "Brak danych");
            }
            databaseSchema = BlockToDBConverter.FromBlockToDBEditVM(model, databaseSchema);
            DatabaseSchemaRepository.Edit(databaseSchema);
            DatabaseSchemaRepository.Save();
        }

        public BlockToDBEditVM GetToEdit(int id)
        {
            DatabaseSchema databaseSchema = DatabaseSchemaRepository.GetSingle(x => x.Id == id);
            BlockToDBEditVM blockToDBEdit = new BlockToDBEditVM()
            {
                Id = databaseSchema.Id,
                Name = databaseSchema.Name,
                Json = databaseSchema.Json,
            };
            return blockToDBEdit;
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