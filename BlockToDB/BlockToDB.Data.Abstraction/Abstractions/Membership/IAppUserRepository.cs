using BlockToDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Data
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        string GetActiveUserIdByEmail(string email);
        int GetUnknownUserId();
    }
}
