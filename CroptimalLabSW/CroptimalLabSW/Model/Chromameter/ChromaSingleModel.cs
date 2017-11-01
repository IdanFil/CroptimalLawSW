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
    class ChromaSingleModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private DBOptions m_DBOptions;
        private ChromaManage m_chromaManage;

        private ObservableCollection<string> _elementsList;
        private ObservableCollection<ChromaResult> _results;
        private string _selectedElementName;
        private Element _selectedElementParams;
        private bool _warmUpEnabled;
        private int _numToAVG;
        private int _repetitions;
        private string _sampleID;
        private double[] _backgroundReading;

        #region Constractors

        public ChromaSingleModel()
        {

        }

        #endregion

        #region Properties

        public ObservableCollection<string> ElementsList
        {
            get { return _elementsList; }
            set
            {
                _elementsList = value;
                RaisePropertyChanged("ElementsList");
            }
        }

        public ObservableCollection<ChromaResult> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                RaisePropertyChanged("Results");
            }
        }

        public string SelectedElementName
        {
            get { return _selectedElementName; }
            set
            {
                _selectedElementName = value;
                RaisePropertyChanged("SelectedElementName");
            }
        }

        public Element SelectedElementParams
        {
            get { return _selectedElementParams; }
            set
            {
                _selectedElementParams = value;
                RaisePropertyChanged("SelectedElementParams");
            }
        }

        public bool WarmUpEnabled
        {
            get { return _warmUpEnabled; }
            set
            {
                _warmUpEnabled = value;
                RaisePropertyChanged("WarmUpEnabled");
            }
        }

        public int NumToAVG
        {
            get { return _numToAVG; }
            set
            {
                _numToAVG = value;
                RaisePropertyChanged("NumToAVG");
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

        public string SampleID
        {
            get { return _sampleID; }
            set
            {
                _sampleID = value;
                RaisePropertyChanged("SampleID");
            }
        }

        public double[] BackgroundReading
        {
            get { return _backgroundReading; }
            set
            {
                _backgroundReading = value;
            }
        }

        #endregion

        #region publicFunction

        public void setElement()
        {
            SelectedElementParams = m_DBOptions.getElementParams(SelectedElementName);
            m_chromaManage.setMaskLEDs(SelectedElementParams.Configuration);
        }

        public void readBG()
        {
            BackgroundReading = m_chromaManage.measureAVG(SelectedElementParams.Configuration, NumToAVG);
        }

        public void measure()
        {
            ChromaResult result = m_chromaManage.measure(SelectedElementParams.Configuration, NumToAVG, BackgroundReading);
            result.SampleID = SampleID;
            Results.Add(result);
        }

        public void removeLastResult()
        {

        }

        public void clearAllResults()
        {

        }

        public void saveResults()
        {

        }

        #endregion


        #region praviteFunction

        private void RaisePropertyChanged(string i_propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(i_propertyName));
            }
        }

        #endregion
    }
}
