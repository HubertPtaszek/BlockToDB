using BlockToDB.Domain;
using DevExtreme.AspNet.Data;

namespace BlockToDB.Data
{
    public interface IDatabaseSchemaRepository : IRepository<DatabaseSchema>
    {
        object GetDatabaseSchemaToList(DataSourceLoadOptionsBase loadOptions);
    }
}