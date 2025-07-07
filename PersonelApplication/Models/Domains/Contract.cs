using PersonelApplication.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Domains
{
    public class Contract
    {
        public Contract()
        {

        }

        public int Id { get; set; }

        public string NumberContract { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal Salary { get; set; }

        public string Comments { get; set; }

        public int TypeContractId { get; set; }

        public int PersonId { get; set; }


        

        public TypeContract TypeContract { get; set; }

        public Person Person { get; set; }



    }
}
