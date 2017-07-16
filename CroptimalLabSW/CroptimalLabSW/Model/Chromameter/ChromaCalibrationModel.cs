using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CroptimalLabSW.Model.DB;

namespace CroptimalLabSW.Model.Chromameter
{
    class ChromaCalibrationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DBOptions m_DBOptions;

        private int _avgNum;
        private double _bgReading;
        private int _repetitions;
        private double _concentration;
        private int _polynomial;
        private double _regression;
        private string _elementName;
        private string _selectedElement;
        private string _SelectedConfiguration;
        private ObservableCollection<string> _elementsList;
        private ObservableCollection<string> _configurationsList;

        #region Constructor
        public ChromaCalibrationModel()
        {
            m_DBOptions = DBOptions.Instance();
            m_DBOptions.NewChromaConfigureAdded += updateConfigurationsList;
            _elementsList = new ObservableCollection<string>(m_DBOptions.getChromameterElementsName());
            ConfigurationsList = m_DBOptions.getChromameterConfigurationNames();
        }
        #endregion

        #region Property
        

        public ObservableCollection<string> ElementsList
        {
            get { return _elementsList; }
            set
            {
                _elementsList = value;
                RaisePropertyChanged("ElementsList");
            }
        }

        public ObservableCollection<string> ConfigurationsList
        {
            get { return _configurationsList; }
            set
            {
                _configurationsList = value;
                RaisePropertyChanged("ConfigurationsList");
            }
        }

        public string SelectedElement
        {
            get { return _selectedElement; }
            set
            {
                _selectedElement = value;
                RaisePropertyChanged("SelectedElement");
            }
        }


        public string ElementName
        {
            get { return _elementName; }
            set
            {
                _elementName = value;
                RaisePropertyChanged("ElementName");
            }
        }


        public double Regression
        {
            get { return _regression; }
            set
            {
                _regression = value;
                RaisePropertyChanged("Regression");
            }
        }


        public int Polynomial
        {
            get { return _polynomial; }
            set
            {
                _polynomial = value;
                RaisePropertyChanged("Polynomial");
            }
        }


        public double Concentration
        {
            get { return _concentration; }
            set
            {
                _concentration = value;
                RaisePropertyChanged("Concentration");
            }
        }


        public int Repetitions
        {
            get { return _repetitions; }
            set
            {
                _repetitions = value;
                RaisePropertyChanged("Repetitions");
            }
        }


        public double BGReading
        {
            get { return _bgReading; }
            set
            {
                _bgReading = value;
                RaisePropertyChanged("BGReading");
            }
        }


        public int AVGNum
        {
            get { return _avgNum; }
            set
            {
                _avgNum = value;
                RaisePropertyChanged("AVGNum");
            }
        }

        public string SelectedConfiguration
        {
            get { return _SelectedConfiguration; }
            set
            {
                _SelectedConfiguration = value;
                RaisePropertyChanged("SelectedConfiguration");
            }
        }

        public void updateConfigurationsList(object sender, EventArgs e)
        {
            ConfigurationsList = m_DBOptions.getChromameterConfigurationNames();
        }

        #endregion

        private void RaisePropertyChanged(string i_propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(i_propertyName));
            }
        }
    }
}
