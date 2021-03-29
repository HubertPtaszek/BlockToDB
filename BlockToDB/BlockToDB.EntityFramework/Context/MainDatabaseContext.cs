using BlockToDB.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

namespace BlockToDB.EntityFramework
{
    public class MainDatabaseContext : IdentityDbContext<AppIdentityUser>
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
            Database.SetInitializer<MainDatabaseContext>(new MigrateDatabaseToLatestVersion<MainDatabaseContext, MainDatabaseMigrations.Configuration>());
        }

        public static IdentityDbContext<AppIdentityUser> Create()
        {
            return new MainDatabaseContext();
        }

        public Guid Id { get; set; }

        public static void MigrateData()
        {
            using (var mainContext = new MainDatabaseContext(true))
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainDatabaseContext, MainDatabaseMigrations.Configuration>());
                mainContext.AppUsers.ToList();
            }
        }

        //Update-Database -configuration BlockToDB.EntityFramework.MainDatabaseMigrations.Configuration -Verbose

        #region Core

        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<Person> Peoples { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }

        #endregion Core

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 5));

            #region Core

            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new AppUserConfiguration());
            modelBuilder.Configurations.Add(new AppRoleConfiguration());
            modelBuilder.Configurations.Add(new AppUserRoleConfiguration());

            #endregion Core

            base.OnModelCreating(modelBuilder);
        }
    }
}