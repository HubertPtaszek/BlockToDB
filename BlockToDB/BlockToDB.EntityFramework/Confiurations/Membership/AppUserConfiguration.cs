using BlockToDB.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.EntityFramework
{
    public class AppUserConfiguration : EntityTypeConfiguration<AppUser>
    {
        public AppUserConfiguration()
        {
        }
    }
}
