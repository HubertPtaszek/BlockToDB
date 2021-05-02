using BlockToDB.Domain;
using DevExtreme.AspNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Data
{
    public interface IDatabaseSchemaRepository : IRepository<DatabaseSchema>
    {
        object GetDatabaseSchemaToList(DataSourceLoadOptionsBase loadOptions);
    }
}
