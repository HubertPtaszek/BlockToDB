using BlockToDB.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BlockToDB.EntityFramework
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            HasOptional(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .WillCascadeOnDelete(false);
            HasOptional(x => x.ModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById)
                .WillCascadeOnDelete(false);
        }
    }
}