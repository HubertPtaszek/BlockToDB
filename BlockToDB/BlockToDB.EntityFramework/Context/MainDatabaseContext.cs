using BlockToDB.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace BlockToDB.EntityFramework
{
    public class MainDatabaseContext : DbContext
    {
        public MainDatabaseContext()
            : base("Name=MainDatabaseContext")
        {
            Id = Guid.NewGuid();
            Database.SetInitializer<MainDatabaseContext>(null);

        }

        public MainDatabaseContext(bool addMigration)
            : base("Name=MainDatabaseContext")
        {
            Id = Guid.NewGuid();
            Database.SetInitializer<MainDatabaseContext>(new MigrateDatabaseToLatestVersion<MainDatabaseContext, BlockToDB.EntityFramework.MainDatabaseMigrations.Configuration>());

        }
        public Guid Id { get; set; }

        public static void MigrateData()
        {
            using (var mainContext = new MainDatabaseContext(true))
            {
                Database.SetInitializer<MainDatabaseContext>(new MigrateDatabaseToLatestVersion<MainDatabaseContext, BlockToDB.EntityFramework.MainDatabaseMigrations.Configuration>());
                mainContext.AppUsers.ToList();
            }
        }

        //Update-Database -configuration BlockToDB.EntityFramework.MainDatabaseMigrations.Configuration -Verbose

        #region Core
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<Person> Peoples { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        #endregion

        #region BlockToDB
        public DbSet<Code> Codes { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 5));

            #region Core
            modelBuilder.Configurations.Add(new LanguageConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new AppUserConfiguration());
            #endregion

            #region BlockToDB
            modelBuilder.Configurations.Add(new CodeConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}

