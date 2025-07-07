namespace PersonelApplication.Migrations
{
    using PersonelApplication.Models.Domains;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PersonelApplication.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PersonelApplication.ApplicationDbContext context)
        {
            context.TypesContract.AddOrUpdate(
                x => x.Id,
                new TypeContract { Id = 1, Name = "Umowa o pracę"},
                new TypeContract { Id = 2, Name = "Umowa zlecenie"},
                new TypeContract { Id = 3, Name = "Umowa o dzieło"},
                new TypeContract { Id = 4, Name = "Inne"}
                );
        }
    }
}
