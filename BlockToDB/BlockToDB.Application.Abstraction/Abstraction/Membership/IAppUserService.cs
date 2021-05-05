using BlockToDB.Application.Abstraction;

namespace BlockToDB.Application
{
    public interface IAppUserService : IService
    {
        AppUserData GetUserDataByWebUserId(string webUserId);

        void Add(AppUserAddVM model);

        void Edit(AppUserEditVM model);

        int GetUnknownUserId();
    }
}