
using PersonelApplication.Migrations;
using PersonelApplication.Models.Configurations;
using PersonelApplication.Models.Domains;
using PersonelApplication.Properties;
using System;
using System.Data.Entity;
using System.Linq;

namespace PersonelApplication
{
    public class ApplicationDbContext : DbContext
    {

        private static string _connectionString = $@"
         Server={Settings.Default.ServerAdress}\{Settings.Default.ServerName};
         Database={Settings.Default.BaseName};
         User Id={Settings.Default.UserServerName};
         Password={Settings.Default.PasswordServer}";


        public ApplicationDbContext()
            : base(_connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>()); 
            Database.Initialize(false); 
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<TypeContract> TypesContract { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new ContractConfiguration());
            modelBuilder.Configurations.Add(new TypeContractConfiguration());
        }
    }

}