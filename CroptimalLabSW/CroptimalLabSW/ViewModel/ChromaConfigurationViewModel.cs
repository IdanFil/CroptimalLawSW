using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CroptimalLabSW.Model.Chromameter;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Resources;
using System.Reflection;
using CroptimalLabSW.Resources;

namespace CroptimalLabSW.ViewModel
{
    public class ChromaConfigurationViewModel : ViewModelBase
    {
        private ChromaConfigurationModel _chromaConfigurationModel;

        private string _errorMessageNewConfName;
        private string _fontColorNewConfName;
        private bool _visibilityErrorMessageNewConfName; 
        private bool _enableAddConf;
        private bool _enableEditConf;
        ResourceManager rm;

        public ChromaConfigurationViewModel()
        {
            rm = new ResourceManager("CroptimalLabSW.Resources.Strings", Assembly.GetExecutingAssembly());
            _chromaConfigurationModel = new ChromaConfigurationModel();
            _chromaConfigurationModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                RaisePropertyChanged(e.PropertyName);
            };
            FontColorNewConfName = "Black";
            VisibilityErrorMessageNewConfName = false;
        }
        public bool VisibilityErrorMessageNewConfName
        {
            get { return _visibilityErrorMessageNewConfName; }
            set
            {
                _visibilityErrorMessageNewConfName = value;
                RaisePropertyChanged("VisibilityErrorMessageNewConfName");
            }
        }

        public string FontColorNewConfName
        {
            get { return _fontColorNewConfName; }
            set
            {
                _fontColorNewConfName = value;
                RaisePropertyChanged("FontColorNewConfName");
            }
        }

        public string ErrorMessageNewConfName
        {
            get { return _errorMessageNewConfName; }
            set
            {
                _errorMessageNewConfName = value;
                RaisePropertyChanged("ErrorMessageNewConfName");
            }
        }

        public bool EnableAddConf
        {
            get { return _enableAddConf; }
            set
            {
                _enableAddConf = value;
                RaisePropertyChanged("EnableAddConf");
            }
        }

        public bool EnableEditConf
        {
            get { return _enableEditConf; }
            set
            {
                _enableEditConf = value;
                RaisePropertyChanged("EnableEditConf");
            }
        }

        public string NewConfName
        {
            get { return _chromaConfigurationModel.NewConfName; }
            set
            {
                _chromaConfigurationModel.NewConfName = value;
                checkNewConfNameValidation();
                RaisePropertyChanged("NewConfName");
            }
        }

        public string SelectedConf
        {
            get { return _chromaConfigurationModel.SelectedConf; }
            set
            {
                _chromaConfigurationModel.SelectedConf = value;
                EnableEditConf = true;
            }
        }

        public ObservableCollection<string> ConfNamesList
        {
            get { return _chromaConfigurationModel.ConfNamesList; }
            set { _chromaConfigurationModel.ConfNamesList = value; }
        }

        public ObservableCollection<bool> ConfParams
        {
            get { return _chromaConfigurationModel.ConfParams; }
            set { _chromaConfigurationModel.ConfParams = value; }
        }

        public RelayCommand AddNewConfiguration
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _chromaConfigurationModel.addNewConfiguration();
                    EnableAddConf = false;
                    NewConfName = "";
                });
            }
        }

        public RelayCommand EditConfCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _chromaConfigurationModel.editNewConfiguration();
                });
            }
        }

        private bool checkNewConfNameValidation()
        {
            if (NewConfName.Equals(""))
            {
                EnableAddConf = false;
                ErrorMessageNewConfName = "";
                FontColorNewConfName = "Black";
                VisibilityErrorMessageNewConfName = true;
                return false;
            }
            if(ConfNamesList.Contains(NewConfName))
            {
                //name exist
                EnableAddConf = false;
                ErrorMessageNewConfName = rm.GetString("ExistsName");
                FontColorNewConfName = "Red";
                VisibilityErrorMessageNewConfName = true;
                return false;
            }
            if(NewConfName.Length == 1)
            {
                //short
                EnableAddConf = false;
                ErrorMessageNewConfName = rm.GetString("ShortName");
                FontColorNewConfName = "Red";
                VisibilityErrorMessageNewConfName = true;
                return false;
            }
            if(!((NewConfName[0] >= 65 && NewConfName[0] <= 91) || (NewConfName[0] >= 97 && NewConfName[0] <= 122)))
            {
                //Invalid name
                EnableAddConf = false;
                ErrorMessageNewConfName = rm.GetString("InvalidName");
                FontColorNewConfName = "Red";
                VisibilityErrorMessageNewConfName = true;
                return false;
            }
            EnableAddConf = true;
            ErrorMessageNewConfName = "";
            FontColorNewConfName = "Green";
            VisibilityErrorMessageNewConfName = false;
            return true;
        }
    }
}
