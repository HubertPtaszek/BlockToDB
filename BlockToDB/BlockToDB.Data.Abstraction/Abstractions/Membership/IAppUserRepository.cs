using BlockToDB.Domain;

namespace BlockToDB.Data
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        int GetUnknownUserId();
    }
}