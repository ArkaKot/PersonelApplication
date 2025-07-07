using PersonelApplication.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Wrappers
{
    public class PersonWrapper : IDataErrorInfo
    {
        public PersonWrapper()
        {
            Group = new GroupWrapper();
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


        public GroupWrapper Group { get; set; }
        public ContractWrapper ContractPerson { get; set; }

        private bool _isFirstNameValid;
        private bool _isLastNameValid;
        private bool _isAgeValid;
        private bool _isPersonIdValid;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Imię jest wymagane";
                            _isFirstNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isFirstNameValid = true;
                        }
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Nazisko jest wymagane";
                            _isLastNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLastNameValid = true;
                        }
                        break;
                    case nameof(Age):
                        if (Age < 18)
                        {
                            Error = "Błędna wartość wieku";
                            _isAgeValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isAgeValid = true;
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
                return _isFirstNameValid && _isLastNameValid && _isAgeValid && Group.IsValid;
            }
        }

    }
}
