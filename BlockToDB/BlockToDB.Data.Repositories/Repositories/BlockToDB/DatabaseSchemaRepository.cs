using BlockToDB.Domain;
using BlockToDB.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;

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
