using BlockToDB.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class AppUserData
    {
        public AppUserData()
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public LanguageDictionary Language { get; set; }
        public string Login { get; set; }
        public bool IsActive { get; set; }
    }
}
