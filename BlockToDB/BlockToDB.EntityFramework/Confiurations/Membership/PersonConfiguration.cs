using BlockToDB.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.EntityFramework
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            HasRequired(x => x.Language)
                .WithMany()
                .HasForeignKey(x => x.LanguageId)
                .WillCascadeOnDelete(false);

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
