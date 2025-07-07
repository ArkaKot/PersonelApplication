using PersonelApplication.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Configurations
{
    public class ContractConfiguration : EntityTypeConfiguration<Contract>
    {
        public ContractConfiguration()
        {
            ToTable("dbo.Contracts");

            HasKey(x => x.Id);
        }
    }
}
