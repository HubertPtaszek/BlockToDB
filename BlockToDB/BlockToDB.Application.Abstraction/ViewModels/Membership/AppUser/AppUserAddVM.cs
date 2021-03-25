using BlockToDB.Dictionaries;
using BlockToDB.Resources.Shared;
using BlockToDB.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class AppUserAddVM
    {
        public string Login { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
    }
}
