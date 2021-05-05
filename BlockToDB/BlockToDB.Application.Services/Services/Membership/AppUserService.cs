using BlockToDB.Data;
using BlockToDB.Domain;
using BlockToDB.Infrastructure;
using BlockToDB.Resources.Shared;
using Ninject;
using System;
using System.Linq;

namespace BlockToDB.Application
{
    public class AppUserService : ServiceBase, IAppUserService
    {
        #region Dependencies

        [Inject]
        public IAppUserRepository AppUserRepository { get; set; }

        [Inject]
        public AppUserConverter AppUserConverter { get; set; }

        #endregion Dependencies

        public AppUserData GetUserDataByWebUserId(string webUserId)
        {
            AppUser user = AppUserRepository.GetSingle(x => x.IsActive && x.AppIdentityUserId == webUserId);
            if (user == null)
                return null;
            AppUserData result = new AppUserData()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AppIdentityUserId = user.AppIdentityUserId
            };
            return result;
        }

        public void Add(AppUserAddVM model)
        {
            if (AppUserRepository.Any(x => x.Email == model.Email))
                throw new BussinesException(1000, ErrorResource.UserAlreadyAdded);
            AppUser user = new AppUser()
            {
                IsActive = model.IsActive,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                AppIdentityUserId = model.AppIdentityUserId
            };
            AppUserRepository.Add(user);
            AppUserRepository.Save();
        }

        public void Edit(AppUserEditVM model)
        {
            AppUser appUser = AppUserRepository.GetSingle(x => x.Id == model.Id);
            if (appUser == null)
            {
                throw new BussinesException(1001, ErrorResource.NoData);
            }
            appUser = AppUserConverter.FromAppUserEditVM(model, appUser);
            AppUserRepository.Edit(appUser);
        }

        public int GetUnknownUserId()
        {
            return AppUserRepository.GetUnknownUserId();
        }
    }
}