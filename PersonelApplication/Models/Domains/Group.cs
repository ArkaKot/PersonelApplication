using PersonelApplication.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Domains
{
    public class Group
    {
        public Group()
        {
            Persons = new Collection<Person>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Person> Persons {  get; set; }

       
    }
}
