using BlockToDB.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BlockToDB.EntityFramework
{
    public class AppUserConfiguration : EntityTypeConfiguration<AppUser>
    {
        public AppUserConfiguration()
        {
            HasRequired(x => x.AppIdentityUser)
                .WithMany()
                .HasForeignKey(x => x.AppIdentityUserId)
                .WillCascadeOnDelete(false);
        }
    }
}