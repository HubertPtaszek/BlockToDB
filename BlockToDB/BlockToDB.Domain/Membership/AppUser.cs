using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockToDB.Domain
{
    [Table("AppUsers")]
    public class AppUser : Person
    {
        public AppUser()
        {
            UserRoles = new List<AppUserRole>();
        }
        public string AppIdentityUserId { get; set; }
        public virtual AppIdentityUser AppIdentityUser { get; set; }

        public virtual List<AppUserRole> UserRoles { get; set; }

    }
}
