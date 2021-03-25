using BlockToDB.Application.Abstraction;

namespace BlockToDB.Application
{
    public interface IAppUserService : IService
    {
        AppUserData GetFirstUser();

        void Add(AppUserAddVM model);

        void Edit(AppUserEditVM model);

        int GetUnknownUserId();

        AppUserData GetUserDataByAdLogin(string userNamePart);

        AppUserListVM GetAppUserListVM();

        void Delete(int id);
    }
}