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
        private ObservableCollection<bool> _confParams;

        private DBOptions m_DBOptions;

        public ChromaConfigurationModel()
        {
            m_DBOptions = new DBOptions();
            getConfigurationsNames();
            ConfParams = new ObservableCollection<bool> { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
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

        public ObservableCollection<bool> ConfParams
        {
            get { return _confParams; }
            set
            {
                _confParams = value;
                RaisePropertyChanged("ConfParams");
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

        public bool getConfiguration()
        {
            ConfParams = m_DBOptions.getConfiguretion(SelectedConf);
            return ConfParams != null ? true : false;
        }

        public bool addNewConfiguration()
        {
            if(m_DBOptions.insertNewChromaConfiguration(NewConfName, ConfParams))
            {
                getConfigurationsNames();
                return true;
            }
            return false;
        }

        public bool editNewConfiguration()
        {
            return m_DBOptions.updateChromaConfiguration(SelectedConf, ConfParams);
        }

        public bool getConfigurationsNames()
        {
            ConfNamesList = m_DBOptions.getChromameterConfigurationNames();
            return ConfNamesList != null ? true : false;
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
