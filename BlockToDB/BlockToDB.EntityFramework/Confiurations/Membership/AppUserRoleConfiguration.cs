using BlockToDB.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BlockToDB.EntityFramework
{
    public class AppUserRoleConfiguration : EntityTypeConfiguration<AppUserRole>
    {
        public AppUserRoleConfiguration()
        {
            HasRequired(x => x.AppRole)
                .WithMany()
                .HasForeignKey(x => x.AppRoleId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.AppUser)
             .WithMany(x => x.UserRoles)
             .HasForeignKey(x => x.AppUserId)
             .WillCascadeOnDelete(false);
        }
    }
}