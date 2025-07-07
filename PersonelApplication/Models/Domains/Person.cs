using PersonelApplication.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Domains
{
    public class Person
    {
        public Person()
        {
            
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public bool Student { get; set; }

        public bool Contract { get; set; }

        public string Comments { get; set; }

        public int GroupId { get; set; }

        public int ContractId { get; set; }


        public string FullName => $"{FirstName} {LastName}";

        public Group Group { get; set; }
        

    }
}
