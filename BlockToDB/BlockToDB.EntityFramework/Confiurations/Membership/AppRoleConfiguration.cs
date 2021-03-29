using BlockToDB.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BlockToDB.EntityFramework
{
    public class AppRoleConfiguration : EntityTypeConfiguration<AppRole>
    {
        public AppRoleConfiguration()
        {
        }
    }
}