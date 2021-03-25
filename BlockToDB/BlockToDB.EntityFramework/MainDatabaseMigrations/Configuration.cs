using BlockToDB.Dictionaries;
using BlockToDB.Domain;
using BlockToDB.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
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
            AddAppSeetings(context);
            AddAppUsers(context);
        }

        #region CoreData
        private void AddCoreData(MainDatabaseContext context)
        {
            if (!context.Languages.Any())
            {
                Language polish = new Language()
                {
                    CultureSymbol = "pl-PL",
                    LanguageDictionary = LanguageDictionary.Polish
                };
                context.Languages.Add(polish);
                context.SaveChanges();
            }
            if (!context.SystemUsers.Any())
            {
                Language polish = context.Languages.FirstOrDefault(x => x.LanguageDictionary == LanguageDictionary.Polish);
                SystemUser admin = new SystemUser()
                {
                    CreatedDate = DateTime.Now,
                    Email = SystemUsers.SystemUserEmail,
                    FirstName = SystemUsers.SystemUserName,
                    IsActive = true,
                    LastName = "",
                    LanguageId = polish.Id,
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
                    LanguageId = polish.Id,
                    Name = SystemUsers.UnknownUserName
                };
                context.SystemUsers.Add(unknownUser);

                context.SaveChanges();
            }
        }
        #endregion

        #region Users

        private void AddAppUsers(MainDatabaseContext context)
        {
            SystemUser sysAdmin = context.SystemUsers.Where(x => x.Name == SystemUsers.SystemUserName).FirstOrDefault();
            if (!context.AppUsers.Any())
            {
                Language polish = context.Languages.FirstOrDefault(x => x.LanguageDictionary == LanguageDictionary.Polish);
                string currentlogin = $"{Environment.MachineName}\\{Environment.UserName}";

                var admin = new AppUser()
                {
                    CreatedById = sysAdmin.Id,
                    CreatedDate = DateTime.Now,
                    Email = "admin@pl.pl",
                    IsActive = true,
                    LastName = "Administrator",
                    FirstName = "",
                    LanguageId = polish.Id,
                    Login = currentlogin
                };
                context.AppUsers.Add(admin);
                context.SaveChanges();
            }
        }
        #endregion UsersRoles

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
