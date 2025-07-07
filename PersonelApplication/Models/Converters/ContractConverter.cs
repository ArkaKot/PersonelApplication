using PersonelApplication.Models.Domains;
using PersonelApplication.Models.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Converters
{
    public static class ContractConverter
    {
        public static ContractWrapper ToWrapper(this Contract model)
        {
            return new ContractWrapper
            {

                Id = model.Id,
                NumberContract = model.NumberContract,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Salary = model.Salary,
                Comments = model.Comments,
                PersonId = model.PersonId,


                TypeContract = new TypeContractWrapper
                {
                    Id = model.TypeContract.Id,
                    Name = model.TypeContract.Name
                },


            };

        }

        public static Contract ToDao(this ContractWrapper model)
        {
            return new Contract
            {
                Id = model.Id,
                NumberContract = model.NumberContract,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Salary = model.Salary,
                Comments = model.Comments,
                PersonId =  model.PersonId,
                TypeContractId = model.TypeContract.Id,
                
            };

        }
    }
}


