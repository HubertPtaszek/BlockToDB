using BlockToDB.Domain;

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
            };

            return result;
        }

        public AppUser FromAppUserEditVM(AppUserEditVM model, AppUser user)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsActive = model.IsActive;
            return user;
        }
    }
}