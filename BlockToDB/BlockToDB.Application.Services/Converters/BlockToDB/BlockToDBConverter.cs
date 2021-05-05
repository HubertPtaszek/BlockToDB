using BlockToDB.Domain;

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
    }
}