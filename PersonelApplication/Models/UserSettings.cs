using PersonelApplication.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models
{
    public class UserSettings : IDataErrorInfo
    {

        private bool _isServerAddressValid;
        private bool _isServerNameValid;
        private bool _isBaseNameValid;
        private bool _isUserServerNameValid;
        private bool _isPasswordServerValid;
        

        public string ServerAdress 
        { 
            get
            {
                return Settings.Default.ServerAdress;
            }
            set
            {
                Settings.Default.ServerAdress = value;
            }
        }
        
        public string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }
            set
            {
                Settings.Default.ServerName = value;
            }
        }
        
        public string BaseName
        {
            get
            {
                return Settings.Default.BaseName;
            }
            set
            {
                Settings.Default.BaseName = value;
            }
        }
        
        public string UserServerName
        {
            get
            {
                return Settings.Default.UserServerName;
            }
            set
            {
                Settings.Default.UserServerName = value;
            }
        }

        public string PasswordServer
        {
            get
            {
                return Settings.Default.PasswordServer;
            }
            set
            {
                Settings.Default.PasswordServer = value;
            }
        }



        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case nameof(ServerAdress):
                        if (string.IsNullOrWhiteSpace(ServerAdress))
                        {
                            Error = "Adres Serwera jest wymagany";
                            _isServerAddressValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerAddressValid = true;
                        }
                        break;

                    case nameof(ServerName):
                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = "Nazwa serwera jest wymagane";
                            _isServerNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerNameValid = true;
                        }
                        break;

                    case nameof(BaseName):
                        if (string.IsNullOrWhiteSpace(BaseName))
                        {
                            Error = "Nazwa Bazy danych jest wymagana";
                            _isBaseNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isBaseNameValid = true;
                        }
                        break;

                    case nameof(UserServerName):
                        if (string.IsNullOrWhiteSpace(UserServerName))
                        {
                            Error = "Użytkownik jest wymagany";
                            _isUserServerNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isUserServerNameValid = true;
                        }
                        break;

                    case nameof(PasswordServer):
                        if (string.IsNullOrWhiteSpace(PasswordServer))
                        {
                            Error = "Hasło jest wymagane";
                            _isPasswordServerValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isPasswordServerValid = true;
                        }
                        break;


                    default:
                        break;
                }
                return Error;
            }
        }
        public string Error  {get; set;}

        public bool IsValid
        {
            get
            {
                return _isServerAddressValid && _isServerNameValid && _isBaseNameValid && _isUserServerNameValid && _isPasswordServerValid;
            }
        }

        public void Save()
        {
            Settings.Default.Save();
        }
    }
}
