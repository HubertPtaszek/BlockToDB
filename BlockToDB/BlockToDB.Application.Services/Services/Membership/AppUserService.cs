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

        public string GetActiveUserIdByEmail(string email)
        {
            return AppUserRepository.GetActiveUserIdByEmail(email);
        }

        public AppUserData GetFirstUser()
        {
            AppUser user = AppUserRepository.GetSingle(x => x.Email == "admin@admin.pl");
            if (user == null)
            {
                user = AppUserRepository.GetAll(x => x.LastName != null && x.LastName != "").OrderBy(x => x.LastName).FirstOrDefault();
            }
            AppUserData result = new AppUserData()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Email,
                IsActive = user.IsActive,
            };
            return result;
        }

        public AppUserListVM GetAppUserListVM()
        {
            AppUserListVM model = new AppUserListVM()
            { };
            return model;
        }

        public AppUserDetailsVM GetAppUserDetailsVM(int userId)
        {
            AppUser crmUser = AppUserRepository.GetSingle(x => x.Id == userId);
            AppUserDetailsVM result = AppUserConverter.ToAppUserDetailsVM(crmUser);
            return result;
        }

        public void Add(AppUserAddVM model)
        {
            if (AppUserRepository.Any(x => x.Email == model.Email))
                throw new BussinesException(1000, ErrorResource.UserAlreadyAdded);
            AppUser user = new AppUser()
            {
                CreatedById = MainContext.PersonId,
                CreatedDate = DateTime.Now,
                IsActive = model.IsActive,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email
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

        public void Delete(int id)
        {
            AppUser appUser = AppUserRepository.GetSingle(x => x.Id == id);
            if (appUser == null)
            {
                throw new BussinesException(1002, ErrorResource.NoData);
            }
        }

        public int GetUnknownUserId()
        {
            return AppUserRepository.GetUnknownUserId();
        }
    }
}