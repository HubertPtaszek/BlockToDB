using System.ComponentModel.DataAnnotations.Schema;

namespace BlockToDB.Domain
{
    [Table("People")]
    public abstract class Person : AuditEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public bool IsActive { get; set; }
        public string MobilePhoneNrPrefix { get; set; }
        public string MobilePhoneNr { get; set; }
        public string LandlinePhoneNrPrefix { get; set; }
        public string LandlinePhoneNr { get; set; }
    }
}
