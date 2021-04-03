using BlockToDB.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BlockToDB.EntityFramework
{
    public class DatabaseSchemaConfiguration : EntityTypeConfiguration<DatabaseSchema>
    {
        public DatabaseSchemaConfiguration()
        {
        }
    }
}