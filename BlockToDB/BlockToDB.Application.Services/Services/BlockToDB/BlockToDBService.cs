using BlockToDB.Data;
using BlockToDB.Domain;
using DevExtreme.AspNet.Data;
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


        public object GetCaseToList(DataSourceLoadOptionsBase loadOptions)
        {
            return DatabaseSchemaRepository.GetDatabaseSchemaToList(loadOptions);
        }

        public void Add(BlockToDBAddVM model)
        {
            DatabaseSchema databaseSchema = new DatabaseSchema()
            {
               Json = model.Json,
               Name = model.Name

            };
            DatabaseSchemaRepository.Add(databaseSchema);
            DatabaseSchemaRepository.Save();

        }
        public void Edit(BlockToDBEditVM model)
        {
            DatabaseSchema databaseSchema = DatabaseSchemaRepository.GetSingle(x => x.Id == model.Id);
            if (databaseSchema == null)
            {
                throw new BussinesException(1001, "Brak danych");
            }

            databaseSchema.Name = model.Name;
            databaseSchema.Json = model.Json;

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


    }
}