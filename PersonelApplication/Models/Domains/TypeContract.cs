using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Domains
{
    public class TypeContract
    {
        public TypeContract()
        {
            Contracts = new Collection<Contract>();
        }

        public int Id { get; set; }

        public string Name { get; set; }


        public ICollection<Contract> Contracts { get; set; }
    }
}
