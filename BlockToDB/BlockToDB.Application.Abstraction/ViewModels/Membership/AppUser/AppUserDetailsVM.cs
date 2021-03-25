using BlockToDB.Resources.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class AppUserDetailsVM
    {
        public int Id { get; set; }
        public AppUserDetailsVM()
        {

        }

        [Display(ResourceType = typeof(SharedResource), Name = "LastName")]
        public string LastName { get; set; }
        [Display(ResourceType = typeof(SharedResource), Name = "FirstName")]
        public string FirstName { get; set; }
        [Display(ResourceType = typeof(SharedResource), Name = "Email")]
        public string Email { get; set; }
        [Display(ResourceType = typeof(SharedResource), Name = "IsActive")]
        public bool IsActive { get; set; }
        [Display(ResourceType = typeof(SharedResource), Name = "Language")]
        public string Language { get; set; }
    }
}
