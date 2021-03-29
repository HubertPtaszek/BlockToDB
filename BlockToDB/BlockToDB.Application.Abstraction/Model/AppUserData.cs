using BlockToDB.Dictionaries;
using System.Collections.Generic;

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
        public string Login { get; set; }
        public bool IsActive { get; set; }
        public List<AppRoleType> Roles { get; set; }
    }
}