using BlockToDB.Domain;
using BlockToDB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class AppUserConverter : ConverterBase
    {
        public AppUserDetailsVM ToAppUserDetailsVM(AppUser user)
        {
            var result = new AppUserDetailsVM()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                Language = user.Language.LanguageDictionary.GetDisplayName()
            };

            return result;
        }
        public AppUser FromAppUserEditVM(AppUserEditVM model, AppUser user)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsActive = model.IsActive;
            user.Login = model.Login;
            return user;
        }
    }
}
