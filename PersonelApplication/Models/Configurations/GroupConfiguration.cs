using PersonelApplication.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace PersonelApplication.Models.Configurations
{
    public class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            ToTable("dbo.Groups");

            HasKey(x => x.Id);
          
            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}
