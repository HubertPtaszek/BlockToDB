using BlockToDB.Data;
using BlockToDB.Dictionaries;
using BlockToDB.Domain;
using BlockToDB.EntityFramework;
using BlockToDB.Infrastructure;
using BlockToDB.Resources.Shared;
using BlockToDB.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class AppUserService : ServiceBase, IAppUserService
    {
        #region Dependencies
        [Inject]
        public IAppUserRepository AppUserRepository { get; set; }
        [Inject]
        public ILanguageRepository LanguageRepository { get; set; }
        [Inject]
        public AppUserConverter AppUserConverter { get; set; }
        #endregion

        public string GetActiveUserIdByEmail(string email)
        {
            return AppUserRepository.GetActiveUserIdByEmail(email);
        }

        public AppUserData GetFirstUser()
        {
            AppUser user = AppUserRepository.GetSingle(x => x.Email == "admin@pl.pl");
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
                Language = user.Language.LanguageDictionary,
                Login = user.Login,
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
            if (AppUserRepository.Any(x => x.Login == model.Login))
                throw new BussinesException(1000, ErrorResource.UserAlreadyAdded);
            Language language = LanguageRepository.GetSingle(x => x.CultureSymbol == "pl-PL");
            AppUser user = new AppUser()
            {
                CreatedById = MainContext.PersonId,
                CreatedDate = DateTime.Now,
                IsActive = model.IsActive,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Login = model.Login,
                LanguageId = language.Id,
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

        public AppUserData GetUserDataByAdLogin(string userNamePart)
        {
            AppUser user = AppUserRepository.GetSingle(x => x.Login == userNamePart);
            if (user == null)
                return null;
            AppUserData result = new AppUserData()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Email,
                Language = user.Language.LanguageDictionary,
                Login = user.Login,
                IsActive = user.IsActive
            };
            return result;
        }
    }
}
