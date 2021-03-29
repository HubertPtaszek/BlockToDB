using BlockToDB.Application.Abstraction;

namespace BlockToDB.Application
{
    public interface IAppUserService : IService
    {
        AppUserData GetFirstUser();

        void Add(AppUserAddVM model);

        void Edit(AppUserEditVM model);

        int GetUnknownUserId();

        AppUserListVM GetAppUserListVM();

        void Delete(int id);
    }
}