using BlockToDB.Dictionaries;
using BlockToDB.Domain;
using BlockToDB.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace BlockToDB.EntityFramework.MainDatabaseMigrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BlockToDB.EntityFramework.MainDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"MainDatabaseMigrations";
        }

        protected override void Seed(MainDatabaseContext context)
        {
            AddCoreData(context);
            AddRoles(context);
            AddAppUsers(context);
            AddAppSeetings(context);
            AddAppUsers(context);
        }

        #region CoreData

        private void AddCoreData(MainDatabaseContext context)
        {
            if (!context.SystemUsers.Any())
            {
                SystemUser admin = new SystemUser()
                {
                    CreatedDate = DateTime.Now,
                    Email = SystemUsers.SystemUserEmail,
                    FirstName = SystemUsers.SystemUserName,
                    IsActive = true,
                    LastName = "",
                    Name = SystemUsers.SystemUserName
                };
                context.SystemUsers.Add(admin);
                context.SaveChanges();
                admin.CreatedById = admin.Id;
                context.Entry(admin).State = EntityState.Modified;

                SystemUser unknownUser = new SystemUser()
                {
                    CreatedDate = DateTime.Now,
                    CreatedById = admin.Id,
                    Email = SystemUsers.UnknownUserEmail,
                    FirstName = SystemUsers.UnknownUserName,
                    IsActive = true,
                    LastName = "",
                    Name = SystemUsers.UnknownUserName
                };
                context.SystemUsers.Add(unknownUser);

                context.SaveChanges();
            }
        }

        #endregion CoreData

        #region Roles

        private void AddRoles(MainDatabaseContext context)
        {
            SystemUser sysAdmin = context.SystemUsers.Where(x => x.Name == SystemUsers.SystemUserName).FirstOrDefault();
            if (!context.AppRoles.Any(x => x.AppRoleType == AppRoleType.Administrator))
            {
                AppRole administratorRole = new AppRole()
                {
                    AppRoleType = AppRoleType.Administrator,
                    Name = "Administrator systemu",
                    Description = "Dostęp do panelu administratora.",
                    CreatedById = sysAdmin.Id,
                    CreatedDate = DateTime.Now
                };
                context.AppRoles.Add(administratorRole);
            }
            if (!context.AppRoles.Any(x => x.AppRoleType == AppRoleType.User))
            {
                AppRole userRole = new AppRole()
                {
                    AppRoleType = AppRoleType.User,
                    Name = "Przychodnia",
                    Description = "Dostęp do panelu użytkownika.",
                    CreatedById = sysAdmin.Id,
                    CreatedDate = DateTime.Now
                };
                context.AppRoles.Add(userRole);
            }
            context.SaveChanges();
        }

        #endregion Roles

        #region Users

        private void AddAppUsers(MainDatabaseContext context)
        {
            SystemUser sysAdmin = context.SystemUsers.Where(x => x.Name == SystemUsers.SystemUserName).FirstOrDefault();
            UserStore<AppIdentityUser> userStore = new UserStore<AppIdentityUser>(context);
            UserManager<AppIdentityUser> userManager = new UserManager<AppIdentityUser>(userStore);
            AppRole adminRole = context.AppRoles.FirstOrDefault(x => x.AppRoleType == AppRoleType.Administrator);
            if (!context.AppUsers.Any(x => x.Email == "admin@admin.pl"))
            {
                AppIdentityUser adminWebUser = CreateWebUser(userManager, sysAdmin, "admin@admin.pl", "Test.1234");
                context.SaveChanges();
                AppUser admin = CreateAppUser(context, sysAdmin, adminWebUser, "", "Administrator");
                context.SaveChanges();
                AppUserRole appUserRole = AddRoleToUser(context, sysAdmin, adminRole, admin);
                context.SaveChanges();
            }
        }

        private static AppIdentityUser CreateWebUser(UserManager<AppIdentityUser> userManager, SystemUser sysAdmin, string email, string password)
        {
            AppIdentityUser webUser = new AppIdentityUser()
            {
                CreatedById = sysAdmin.CreatedById,
                CreatedDate = DateTime.Now,
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                Id = Guid.NewGuid().ToString()
            };
            userManager.Create(webUser, password);
            return webUser;
        }

        private static AppUser CreateAppUser(MainDatabaseContext context, SystemUser sysAdmin, AppIdentityUser webUser, string firstName, string lastName)
        {
            AppUser user = new AppUser()
            {
                CreatedById = sysAdmin.CreatedById,
                CreatedDate = DateTime.Now,
                Email = webUser.Email,
                IsActive = true,
                LastName = lastName,
                FirstName = firstName,
                AppIdentityUserId = webUser.Id,
            };
            context.AppUsers.Add(user);
            return user;
        }

        private static AppUserRole AddRoleToUser(MainDatabaseContext context, SystemUser sysAdmin, AppRole role, AppUser user)
        {
            AppUserRole appUserRole = new AppUserRole() { CreatedById = sysAdmin.CreatedById, CreatedDate = DateTime.Now, AppRoleId = role.Id, AppUserId = user.Id, };
            context.AppUserRoles.Add(appUserRole);
            return appUserRole;
        }

        #endregion Users

        #region AppSetting

        private void AddAppSeetings(MainDatabaseContext context)
        {
            if (!context.AppSettings.Any(r => r.Type == AppSettingEnum.ApplicationWebAddress))
            {
                AppSetting setting = new AppSetting();
                setting.Type = AppSettingEnum.ApplicationWebAddress;
                setting.Value = "http://localhost:58605/";
                context.AppSettings.Add(setting);
            }
            context.SaveChanges();
        }

        #endregion AppSetting
    }
}