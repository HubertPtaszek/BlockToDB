using BlockToDB.Dictionaries;
using BlockToDB.Domain;
using BlockToDB.EntityFramework;
using BlockToDB.Infrastructure;
using BlockToDB.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Data
{
    public class AppUserRepository : Repository<AppUser, MainDatabaseContext>, IAppUserRepository
    {

        public AppUserRepository(MainDatabaseContext context)
         : base(context)
        {

        }

        public string GetActiveUserIdByEmail(string email)
        {
            email = email.Trim().ToLower();
            return null;
            //return _dbset.Where(x => x.IsActive && x.Email == email).Select(x => x.WebUserId).FirstOrDefault();
        }

        public int GetUnknownUserId()
        {
            return Context.SystemUsers.FirstOrDefault(x => x.Email == SystemUsers.UnknownUserEmail).Id;
        }
    }
}
