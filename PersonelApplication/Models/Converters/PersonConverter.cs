using PersonelApplication.Models.Domains;
using PersonelApplication.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Converters
{


    public static class PersonConverter
    {
        public static PersonWrapper ToWrapper(this Person model)
        {
            return new PersonWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                Student = model.Student,
                Contract = model.Contract,
                Comments = model.Comments,
                ContractId = model.ContractId,

                Group = new GroupWrapper
                {
                    Id = model.Group.Id,
                    Name = model.Group.Name
                },

                //ContractPerson = new ContractWrapper
                //{
                //   Id = model.ContractId,
                //},


            };  
        }

        public static Person ToDao(this PersonWrapper model)
        {
            return new Person
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                Student = model.Student,
                Contract = model.Contract,
                Comments = model.Comments,
                GroupId = model.Group.Id,
                ContractId = model.ContractId
            
            };

        }
    }
}
