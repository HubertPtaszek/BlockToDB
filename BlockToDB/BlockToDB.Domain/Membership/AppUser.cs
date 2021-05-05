using System.ComponentModel.DataAnnotations.Schema;

namespace BlockToDB.Domain
{
    [Table("AppUsers")]
    public class AppUser : Person
    {
        public AppUser()
        {
        }

        public string AppIdentityUserId { get; set; }
        public virtual AppIdentityUser AppIdentityUser { get; set; }
    }
}