using System.ComponentModel.DataAnnotations.Schema;

namespace BlockToDB.Domain
{
    [Table("SystemUsers")]
    public class SystemUser : Person
    {
        public string Name { get; set; }
    }
}
