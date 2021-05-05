using BlockToDB.Domain;
using BlockToDB.EntityFramework;
using BlockToDB.Utils;
using System.Linq;

namespace BlockToDB.Data
{
    public class AppUserRepository : Repository<AppUser, MainDatabaseContext>, IAppUserRepository
    {
        public AppUserRepository(MainDatabaseContext context)
         : base(context)
        {
        }

        public int GetUnknownUserId()
        {
            return Context.SystemUsers.FirstOrDefault(x => x.Email == SystemUsers.UnknownUserEmail).Id;
        }
    }
}