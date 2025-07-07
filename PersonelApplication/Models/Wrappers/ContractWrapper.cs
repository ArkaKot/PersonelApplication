using PersonelApplication.Models.Domains;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Wrappers
{
    public class ContractWrapper : IDataErrorInfo
    {
        public ContractWrapper()
        {
            TypeContract = new TypeContractWrapper();
        }

        public int Id { get; set; }

        public string NumberContract { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal Salary { get; set; }

        public string Comments { get; set; }

        public int TypeContractId { get; set; }

        public int PersonId { get; set; }

        public TypeContractWrapper TypeContract { get; set; }

        public PersonWrapper Person { get; set; }

        private bool _isNumberContractValid;
        private bool _isStartDateValid;
        private bool _isSalaryValid;
        private bool _isPersonValid;


        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(NumberContract):
                        if (string.IsNullOrWhiteSpace(NumberContract))
                        {
                            Error = "Numer Umowy jest wymagany.";
                            _isNumberContractValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isNumberContractValid = true;
                        }
                        break;
                    case nameof(StartDate):
                        if (StartDate == null || StartDate.Year <= 2010)
                        {
                            Error = "Nieprawidłowa wartość";
                            _isStartDateValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isStartDateValid = true;
                        }
                        break;
                    case nameof(Salary):
                        if (Salary <= 0)
                        {
                            Error = "Wynagrodzenie ma błędną wartość";
                            _isSalaryValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isSalaryValid = true;
                        }
                        break;
                    case nameof(PersonId):
                        if (PersonId == 0)
                        {
                            Error = "Wybierz osobę";
                            _isPersonValid = false;
                        }
                        else 
                        {
                            Error = string.Empty;
                            _isPersonValid = true;
                        }
                        break;
                        
                    default:
                        break;
                }
                return Error;
            }
        }

        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return _isNumberContractValid && _isStartDateValid && _isSalaryValid && _isPersonValid;;
            }
        }
    }
}
