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
using MahApps.Metro.Controls.Dialogs;

namespace CroptimalLabSW.ViewModel
{
    public class ChromaConfigurationViewModel : ViewModelBase
    {
        private ChromaConfigurationModel _chromaConfigurationModel;
        private IDialogCoordinator dialogCoordinator;

        private string _errorMessageNewConfName;
        private string _fontColorNewConfName;
        private bool _visibilityErrorMessageNewConfName; 
        private bool _enableAddConf;
        private bool _enableEditConf;
        private ObservableCollection<string> _detectorsList;
        ResourceManager rm;

        public ChromaConfigurationViewModel()
        {
            dialogCoordinator = DialogCoordinator.Instance;
            DetectorsList = new ObservableCollection<string>() {"","1","2","3","4","5","6","7","8"};
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

        public int WarmUpSec
        {
            get { return _chromaConfigurationModel.WarmUpSec; }
            set
            {
                _chromaConfigurationModel.WarmUpSec = value;
                RaisePropertyChanged("WarmUpSec");
            }
        }

        public ObservableCollection<string> DetectorsList
        {
            get { return _detectorsList; }
            set
            {
                _detectorsList = value;
                RaisePropertyChanged("DetectorsList");
            }
        }

        public ObservableCollection<bool> SelectedLEDs
        {
            get { return _chromaConfigurationModel.SelectedLEDs; }
            set
            {
                _chromaConfigurationModel.SelectedLEDs = value;
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

        public ObservableCollection<int> ConfParams
        {
            get { return _chromaConfigurationModel.ConfParams; }
            set { _chromaConfigurationModel.ConfParams = value;
            }
        }

        public RelayCommand AddNewConfiguration
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MessageDialogResult x = dialogCoordinator.ShowModalMessageExternal(this, "Message", "Are you sure you want to add configuration?", MessageDialogStyle.AffirmativeAndNegative);
                    if (x == MessageDialogResult.Affirmative)
                    {
                        if (checkSelectedConf())
                        {
                            if (_chromaConfigurationModel.addNewConfiguration())
                            {
                                dialogCoordinator.ShowMessageAsync(this, "Message", "Configuration added.", MessageDialogStyle.Affirmative);
                                EnableAddConf = false;
                                NewConfName = "";
                            }
                            else
                            {
                                dialogCoordinator.ShowMessageAsync(this, "Error", "Add calibration failed, please check logs file.", MessageDialogStyle.Affirmative);
                            }
                        }
                    }
                });
            }
        }

        public RelayCommand EditConfCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MessageDialogResult x = dialogCoordinator.ShowModalMessageExternal(this, "Message", "Are you sure you want to update?", MessageDialogStyle.AffirmativeAndNegative);
                    if (x == MessageDialogResult.Affirmative)
                    {
                        if (checkSelectedConf())
                        {
                            if (_chromaConfigurationModel.editNewConfiguration())
                            {
                                dialogCoordinator.ShowMessageAsync(this, "Message", "Configuration updated.", MessageDialogStyle.Affirmative);
                            }
                            else
                            {
                                dialogCoordinator.ShowMessageAsync(this, "Error", "Edit calibration failed, please check logs file.", MessageDialogStyle.Affirmative);
                            }
                        }
                    }
                });
            }
        }

        public RelayCommand SaveWarmUpCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if(_chromaConfigurationModel.saveWarmUp())
                    {
                        dialogCoordinator.ShowMessageAsync(this, "Message", "Warm up time updated.", MessageDialogStyle.Affirmative);
                    }
                    else
                    {
                        dialogCoordinator.ShowMessageAsync(this, "Error", "Save warm up time failed, please check logs file.", MessageDialogStyle.Affirmative);
                    }
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

        private bool checkSelectedConf()
        {
            int LEDsCount = 0;
            bool[] usedDetectors = new bool[] { false, false, false, false, false, false, false, false, false };

            for(int i = 0; i < SelectedLEDs.Count; i++)
            {
                if (SelectedLEDs[i])
                {
                    LEDsCount++;
                    if(ConfParams[i] == 0)
                    {
                        dialogCoordinator.ShowMessageAsync(this, "Error", "Please select detector.", MessageDialogStyle.Affirmative);
                        return false;
                    }
                    if (usedDetectors[ConfParams[i]])
                    {
                        dialogCoordinator.ShowMessageAsync(this, "Error", "Detector can be used only one LED.", MessageDialogStyle.Affirmative);
                        return false;
                    }
                    usedDetectors[ConfParams[i]] = true;
                }
            }
            if(LEDsCount == 0)
            {
                dialogCoordinator.ShowMessageAsync(this, "Error", "You must select at least one LED.", MessageDialogStyle.Affirmative);
                return false;
            }
            return true;
        }
    }
}
