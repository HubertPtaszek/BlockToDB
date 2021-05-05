using BlockToDB.Domain;
using BlockToDB.EntityFramework;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using System.Linq;

namespace BlockToDB.Data
{
    public class DatabaseSchemaRepository : Repository<DatabaseSchema, MainDatabaseContext>, IDatabaseSchemaRepository
    {
        public DatabaseSchemaRepository(MainDatabaseContext context)
            : base(context)
        {
        }

        public object GetDatabaseSchemaToList(DataSourceLoadOptionsBase loadOptions)
        {
            var query = _dbset.Where(x => x.CreatedById == MainContext.PersonId).Select(x => new
            {
                x.Id,
                x.Name,
                x.Json,
            });
            LoadResult result = DataSourceLoader.Load(query, loadOptions);
            return result;
        }
    }
}