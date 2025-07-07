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
    public class TypeContractConfiguration : EntityTypeConfiguration<TypeContract>
    {
        public TypeContractConfiguration()
        {
            ToTable("dbo.TypesContract");

            Property(x => x.Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
