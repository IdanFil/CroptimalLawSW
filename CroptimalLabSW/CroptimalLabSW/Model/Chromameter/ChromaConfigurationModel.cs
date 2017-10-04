using CroptimalLabSW.Model.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CroptimalLabSW.Model.Chromameter
{
    class ChromaConfigurationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<string> _confNamesList;
        private string _selectedConf;
        private string _newConfName;
        private int _warmUpSec;
        private ObservableCollection<int> _confParams;
        private ObservableCollection<bool> _selectedLEDs;

        private DBOptions m_DBOptions;

        public ChromaConfigurationModel()
        {
            m_DBOptions = DBOptions.Instance();
            m_DBOptions.NewChromaConfigureAdded += updateConfigurationsList;
            SelectedLEDs = new ObservableCollection<bool> { false, false, false, false, false, false, false, false };
            ConfParams = new ObservableCollection<int> { 0,0,0,0,0,0,0,0};
            WarmUpSec = m_DBOptions.getChromameterWarmUpSec();
            ConfNamesList = m_DBOptions.getChromameterConfigurationNames();
            
        }

        public int WarmUpSec
        {
            get { return _warmUpSec; }
            set
            {
                _warmUpSec = value;
                RaisePropertyChanged("WarmUpSec");
            }
        }

        public ObservableCollection<string> ConfNamesList
        {
            get { return _confNamesList; }
            set
            {
                _confNamesList = value;
                RaisePropertyChanged("ConfNamesList");
            }
        }

        public ObservableCollection<int> ConfParams
        {
            get { return _confParams; }
            set
            {
                _confParams = value;
                updateSelectedLEDs();
                RaisePropertyChanged("ConfParams");
            }
        }

        public ObservableCollection<bool> SelectedLEDs
        {
            get { return _selectedLEDs; }
            set
            {
                _selectedLEDs = value;
                RaisePropertyChanged("SelectedLEDs");
            }
        }

        public string NewConfName
        {
            get { return _newConfName; }
            set
            {
                _newConfName = value;
                RaisePropertyChanged("NewConfName");
            }
        }

        public string SelectedConf
        {
            get { return _selectedConf; }
            set
            {
                _selectedConf = value;
                getConfiguration();
            }
        }

        public void updateConfigurationsList(object sender, EventArgs e)
        {
            ConfNamesList = m_DBOptions.getChromameterConfigurationNames();
        }

        public bool getConfiguration()
        {
            ConfParams = m_DBOptions.getConfiguretion(SelectedConf);
            return ConfParams != null ? true : false;
        }

        public bool addNewConfiguration()
        {
            return m_DBOptions.insertChromaConfiguration(NewConfName, ConfParams);
        }

        public bool saveWarmUp()
        {
            if (m_DBOptions.updateChromaWarmUpSec(WarmUpSec))
            {
                return true;
            }
            return false;
        }

        public bool editNewConfiguration()
        {
            return m_DBOptions.updateChromaConfiguration(SelectedConf, ConfParams);
        }

        private void updateSelectedLEDs()
        {
            for(int i = 0; i < ConfParams.Count; i++)
            {
                if(ConfParams[i] > 0)
                {
                    SelectedLEDs[i] = true;
                }
                else
                {
                    SelectedLEDs[i] = false;  
                }
            }
        }

        private void RaisePropertyChanged(string i_propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(i_propertyName));
            }
        }
    }
}
