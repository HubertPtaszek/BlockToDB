using BlockToDB.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BlockToDB.EntityFramework
{
    public class AppUserConfiguration : EntityTypeConfiguration<AppUser>
    {
        public AppUserConfiguration()
        {
            HasMany(x => x.UserRoles)
                .WithRequired(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.AppIdentityUser)
                .WithMany()
                .HasForeignKey(x => x.AppIdentityUserId)
                .WillCascadeOnDelete(false);
        }
    }
}