using PersonelApplication.Models.Domains;
using PersonelApplication.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PersonelApplication.Models.Converters;
using System.Data.Entity.Validation;

namespace PersonelApplication
{
    public class Repository
    {

        public List<ContractWrapper> GetContracts(int TypeContractId)
        {
            using (var context = new ApplicationDbContext())
            {
                var conntracts = context
                    .Contracts
                    .Include(x => x.TypeContract)
                    .AsQueryable();

                if (TypeContractId != 0)
                    conntracts = conntracts.Where(x => x.TypeContractId == TypeContractId);

                return conntracts.ToList().Select(x => x.ToWrapper()).ToList();
            }
        }

        public List<PersonWrapper> GetPersons(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var persons = context
                    .Persons
                    .Include(x => x.Group)
                    .AsQueryable();

                if (groupId != 0)
                    persons = persons.Where(x => x.GroupId == groupId);


                return persons.ToList().Select(x => x.ToWrapper()).ToList();
            }
        }

        public void DissmisPerson(int id, DateTime endDate, int contractId)
        {
            using (var context = new ApplicationDbContext())
            {
                var personToDissmis = context.Persons.Find(id);

                personToDissmis.Contract = false;
                personToDissmis.ContractId = 0;

                var contractToEditDate = context.Contracts.Find(contractId);
                contractToEditDate.EndDate = endDate;
                contractToEditDate.Comments = "Zwolniony";
                context.SaveChanges();
            }
        }

        public void UpdatePerson(PersonWrapper personWrapper)
        {
            var person = personWrapper.ToDao();

            using (var context = new ApplicationDbContext())
            {
                UpdatePersonProperties(person, context);

                context.SaveChanges();
            }
        }

        public void UpdateContract(ContractWrapper contractWrapper)
        {
            var contract = contractWrapper.ToDao();

            using (var context = new ApplicationDbContext())
            {
                UpdateContractProperties(contract, context);

                context.SaveChanges();
            }
        }

        private void UpdateContractProperties(Contract contract, ApplicationDbContext context)
        {
            var contractToUpdate = context.Contracts.Find(contract.Id);

            contractToUpdate.NumberContract = contract.NumberContract;
            contractToUpdate.StartDate = contract.StartDate;
            contractToUpdate.EndDate = contract.EndDate;
            contractToUpdate.Salary = contract.Salary;
            contractToUpdate.Comments = contract.Comments;
            contractToUpdate.TypeContractId = contract.TypeContractId;
            contractToUpdate.PersonId = contract.PersonId;

        }

        private void UpdatePersonProperties(Person person, ApplicationDbContext context)
        {
            var personToUpdate = context.Persons.Find(person.Id);

            personToUpdate.FirstName = person.FirstName;
            personToUpdate.LastName = person.LastName;
            personToUpdate.Age = person.Age;
            personToUpdate.Comments = person.Comments;
            personToUpdate.Student = person.Student;
            personToUpdate.GroupId = person.GroupId;
            //personToUpdate.ContractId = person.ContractId;
        }

        public void AddPerson(PersonWrapper personWrapper)
        {
            var person = personWrapper.ToDao();

            using (var context = new ApplicationDbContext())
            {
                var dbPerson = context.Persons.Add(person);

                context.SaveChanges();
            }
        }

        public void AddContract(ContractWrapper contractWrapper)
        {
            var contract = contractWrapper.ToDao();


            using (var context = new ApplicationDbContext())
            {

                var dbContract = context.Contracts.Add(contract);
                context.SaveChanges();
            }

            UpdateContractIdInPerson(contract.PersonId, contract.Id);
            UpdateContractToPerson(contract.PersonId);
        }

        private void UpdateContractToPerson(int personId)
        {
            using (var context = new ApplicationDbContext())
            {
                var contractUpdateInPerson = context.Persons.Find(personId);
                contractUpdateInPerson.Contract = true;
                context.SaveChanges();
            }
        }

        public List<Person> GetPersonsToContract()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Persons.ToList();
            }
        }

        public List<Person> GetPersonsAddToContract()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Persons.Where(x => x.ContractId == 0).ToList();
            }
        }

        public List<TypeContract> GetTypeContracts()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.TypesContract.ToList();
            }
        }

        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }
        }

        public void DeleteContract(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var contractToDelete = context.Contracts.Find(id);
                context.Contracts.Remove(contractToDelete);
                context.SaveChanges();
            }
        }

        public void UpdateContractIdInPerson(int personId, int contractId)
        {
            using (var context = new ApplicationDbContext())
            {
                var personToUpdate = context.Persons.Find(personId);
                personToUpdate.ContractId = contractId;
                context.SaveChanges();
            }
        }

        public void AddGroup(GroupWrapper groupWrapper)
        {
            var group = groupWrapper.ToDao();


            using (var context = new ApplicationDbContext())
            {

                var dbGroup = context.Groups.Add(group);
                context.SaveChanges();
            }
        }

        public void DeleteGroup(int selectedGroupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var groupToDelete = context.Groups.Find(selectedGroupId);
                context.Groups.Remove(groupToDelete);
                context.SaveChanges();
            }
            
        }
    }
}
