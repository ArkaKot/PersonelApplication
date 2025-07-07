
using PersonelApplication.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Configurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            ToTable("dbo.Persons");

            HasKey(x => x.Id);
        }
    }
}
