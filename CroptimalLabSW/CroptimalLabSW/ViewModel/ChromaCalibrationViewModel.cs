using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using CroptimalLabSW.Messages;
using GalaSoft.MvvmLight;
using OxyPlot;
using OxyPlot.Axes;
using CroptimalLabSW.Model.Chromameter;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls.Dialogs;
using System.Resources;
using System.Reflection;

namespace CroptimalLabSW.ViewModel
{
    public class ChromaCalibrationViewModel : ViewModelBase
    {
        private ChromaCalibrationModel m_chromaCalibrationModel;
        private IDialogCoordinator dialogCoordinator;
        private bool _warmUpEnabled;
        private string _errorMessageElementName;
        private bool _visibilityErrorMessageElementName;
        private string _fontColorElementName;
        private bool _enableElement;
        private bool _newElementChecked;

        private bool _setConfig_IsEnabled;

        private bool _AVGtBox_IsEnabled;
        private bool _repConcBox_IsEnabled;
        private bool _readBG_IsEnabled;
        private bool _measure_IsEnabled;
        private bool _polyCBox_IsEnabled;
        private bool _calculate_IsEnabled;
        private bool _save_IsEnabled;

        ResourceManager rm;

        #region Constructor
        public ChromaCalibrationViewModel()
        {
            dialogCoordinator = DialogCoordinator.Instance;
            m_chromaCalibrationModel = new ChromaCalibrationModel();
            rm = new ResourceManager("CroptimalLabSW.Resources.Strings", Assembly.GetExecutingAssembly());
            WarmUpEnabled = false;
            FontColorElementName = "Black";
            VisibilityErrorMessageElementName = false;
            m_chromaCalibrationModel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                RaisePropertyChanged(e.PropertyName);
            };
        }

        #endregion

        #region Property

        public ObservableCollection<string> ElementsList
        {
            get { return m_chromaCalibrationModel.ElementsList; }
            set
            {
                m_chromaCalibrationModel.ElementsList = value;
                RaisePropertyChanged("ElementsList");
            }
        }

        public PlotModel PlotModel
        {
            get
            {
                return m_chromaCalibrationModel.PlotModel;
            }
            set
            {
                m_chromaCalibrationModel.PlotModel = value;
                RaisePropertyChanged("PlotModel");
            }
        }

        public string SelectedElement
        {
            get { return m_chromaCalibrationModel.SelectedElement; }
            set
            {
                m_chromaCalibrationModel.SelectedElement = value;
                if(value != "")
                {
                    EnableElement = true;
                }
                RaisePropertyChanged("SelectedElement");
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
        


        public string NewElementName
        {
            get { return m_chromaCalibrationModel.NewElementName; }
            set
            {
                m_chromaCalibrationModel.NewElementName = value;
                checkNewElementNameValidation();
                RaisePropertyChanged("NewElementName");
            }
        }


        public double Regression
        {
            get { return m_chromaCalibrationModel.Regression; }
            set
            {
                m_chromaCalibrationModel.Regression = value;
                RaisePropertyChanged("Regression");
            }
        }


        public int PolynomialOrder
        {
            get { return m_chromaCalibrationModel.PolynomialOrder; }
            set
            {
                m_chromaCalibrationModel.PolynomialOrder = value;
                RaisePropertyChanged("PolynomialOrder");
            }
        }


        public double? Concentration
        {
            get { return m_chromaCalibrationModel.Concentration; }
            set
            {
                m_chromaCalibrationModel.Concentration = value;
                RaisePropertyChanged("Concentration");
            }
        }


        public int? Repetitions
        {
            get { return m_chromaCalibrationModel.Repetitions; }
            set
            {
                m_chromaCalibrationModel.Repetitions = value;
                RaisePropertyChanged("Repetitions");
            }
        }


        public double[] BGReading
        {
            get { return m_chromaCalibrationModel.BGReading; }
            set
            {
                m_chromaCalibrationModel.BGReading = value;
                RaisePropertyChanged("BGReading");
            }
        }


        public ObservableCollection<Measurement> Measurments
        {
            get { return m_chromaCalibrationModel.Measurments; }
            set
            {
                m_chromaCalibrationModel.Measurments = value;
              //  printMeasurementsToGraph();
                RaisePropertyChanged("Measurments");
            }
        }

        public int? AVGNum
        {
            get { return m_chromaCalibrationModel.AVGNum; }
            set
            {
                m_chromaCalibrationModel.AVGNum = value;
                RaisePropertyChanged("AVGNum");
            }
        }

        public string SelectedConf
        {
            get { return m_chromaCalibrationModel.SelectedConf; }
            set
            {
                m_chromaCalibrationModel.SelectedConf = value;
                RaisePropertyChanged("SelectedConf");
            }
        }

        public ObservableCollection<string> ConfigurationsList
        {
            get { return m_chromaCalibrationModel.ConfigurationsList; }
            set
            {
                m_chromaCalibrationModel.ConfigurationsList = value;
                RaisePropertyChanged("ConfigurationsList");
            }
        }

        public ObservableCollection<int> PolynomialOrderOptions
        {
            get { return m_chromaCalibrationModel.PolynomialOrderOptions; }
            set
            {
                m_chromaCalibrationModel.PolynomialOrderOptions = value;
                RaisePropertyChanged("PolynomialOrderOptions");
            }
        }

        public string ErrorMessageElementName
        {
            get { return _errorMessageElementName; }
            set
            {
                _errorMessageElementName = value;
                RaisePropertyChanged("ErrorMessageElementName");
            }
        }

        public bool SetConfig_IsEnabled
        {
            get { return _setConfig_IsEnabled; }
            set
            {
                _setConfig_IsEnabled = value;
                RaisePropertyChanged("SetConfigIsEnabled");
            }
        }

        public bool AVGtBox_IsEnabled
        {
            get { return _AVGtBox_IsEnabled; }
            set
            {
                _AVGtBox_IsEnabled = value;
                RaisePropertyChanged("AVGtBox_IsEnabled");
            }
        }

        public bool RepConcBox_IsEnabled
        {
            get { return _repConcBox_IsEnabled; }
            set
            {
                _repConcBox_IsEnabled = value;
                RaisePropertyChanged("RepConcBox_IsEnabled");
            }
        }

        public bool ReadBG_IsEnabled
        {
            get { return _readBG_IsEnabled; }
            set
            {
                _readBG_IsEnabled = value;
                RaisePropertyChanged("ReadBG_IsEnabled");
            }
        }

        public bool Measure_IsEnabled
        {
            get { return _measure_IsEnabled; }
            set
            {
                _measure_IsEnabled = value;
                RaisePropertyChanged("Measure_IsEnabled");
            }
        }

        public bool PolyCBox_IsEnabled
        {
            get { return _polyCBox_IsEnabled; }
            set
            {
                _polyCBox_IsEnabled = value;
                RaisePropertyChanged("PolyCBox_IsEnabled");
            }
        }

        public bool Calculate_IsEnabled
        {
            get { return _calculate_IsEnabled; }
            set
            {
                _calculate_IsEnabled = value;
                RaisePropertyChanged("Calculate_IsEnabled");
            }
        }

        public bool Save_IsEnabled
        {
            get { return _save_IsEnabled; }
            set
            {
                _save_IsEnabled = value;
                RaisePropertyChanged("Save_IsEnabled");
            }
        }



        public bool VisibilityErrorMessageElementName
        {
            get { return _visibilityErrorMessageElementName; }
            set
            {
                _visibilityErrorMessageElementName = value;
                RaisePropertyChanged("VisibilityErrorMessageElementName");
            }
        }

        public string FontColorElementName
        {
            get { return _fontColorElementName; }
            set
            {
                _fontColorElementName = value;
                RaisePropertyChanged("FontColorElementName");
            }
        }

        public bool EnableElement
        {
            get { return _enableElement; }
            set
            {
                _enableElement = value;
                RaisePropertyChanged("EnableElement");
            }
        }

        public bool NewElementChecked
        {
            get { return _newElementChecked; }
            set
            {
                _newElementChecked = value;
                if(_newElementChecked == true)
                {
                    EnableElement = false;
                }
                else
                {
                    NewElementName = "";
                    if (SelectedElement != "")
                    {
                        EnableElement = true;
                    }
                    else
                    {
                        EnableElement = false;
                    }
                }
                RaisePropertyChanged("NewElementChecked");
            }
        }
        

        #endregion

        #region Commands

        public RelayCommand setConfCommand
        {
            get
            {
                return new RelayCommand(startConf);
            }
        }

        public RelayCommand readBGCommand
        {
            get
            {
                return new RelayCommand(m_chromaCalibrationModel.readBG);
            }
        }

        public RelayCommand measureCommand
        {
            get
            {
                return new RelayCommand(measureAndPlotUpdate);
            }
        }

        public RelayCommand calculateCommand
        {
            get
            {
                return new RelayCommand(m_chromaCalibrationModel.calculate);
            }
        }

        public RelayCommand saveCalibrationCommand
        {
            get
            {
                return new RelayCommand(saveCalibration);
            }
        }

        

        #endregion

        public void initPage()
        {
            SetConfig_IsEnabled = false;
            Save_IsEnabled = false;
            Calculate_IsEnabled = false;
            PolyCBox_IsEnabled = false;
            Measure_IsEnabled = false;
            ReadBG_IsEnabled = false;
            RepConcBox_IsEnabled = false;
            AVGtBox_IsEnabled = false;

            m_chromaCalibrationModel.initParameters();
        }

        private void saveCalibration()
        {
            if(_newElementChecked)
            {
                m_chromaCalibrationModel.addNewElement();
            }
            else
            {
                m_chromaCalibrationModel.modifyElement();
            }
        }

        private void measureAndPlotUpdate()
        {
            m_chromaCalibrationModel.measure();
            RaisePropertyChanged("PlotModel");
        }


        //private void printMeasurementsToGraph()
        //{
        //    OxyPlot.Series.LineSeries punktySerii = new OxyPlot.Series.LineSeries();
        //    PlotModel.Series.Clear();
        //    foreach (Measurement measure in Measurments)
        //    {
        //        punktySerii.Points.Add(new OxyPlot.DataPoint(measure.Concentration, measure.Absorption));
        //    }
        //    _plotModel.Series.Add(punktySerii);
        //}

        private void startConf()
        {
            if (m_chromaCalibrationModel.SelectedConf == "")
            {
                dialogCoordinator.ShowMessageAsync(this, "Error", "Please select configuration.", MessageDialogStyle.Affirmative);
            }
            else
            {
                m_chromaCalibrationModel.setConf();
                if (_warmUpEnabled)
                {
                    //wait for warmup
                }
            }
        }

        #region validation

        private bool checkNewElementNameValidation()
        {
            if (NewElementName.Equals(""))
            {
                EnableElement = false;
                ErrorMessageElementName = "";
                FontColorElementName = "Black";
                VisibilityErrorMessageElementName = true;
                return false;
            }
            if (ElementsList.Contains(NewElementName))
            {
                //name exist
                EnableElement = false;
                ErrorMessageElementName = rm.GetString("ExistsName");
                FontColorElementName = "Red";
                VisibilityErrorMessageElementName = true;
                return false;
            }
            if (NewElementName.Length == 1)
            {
                //short
                EnableElement = false;
                ErrorMessageElementName = rm.GetString("ShortName");
                FontColorElementName = "Red";
                VisibilityErrorMessageElementName = true;
                return false;
            }
            if (!((NewElementName[0] >= 65 && NewElementName[0] <= 91) || (NewElementName[0] >= 97 && NewElementName[0] <= 122)))
            {
                //Invalid name
                EnableElement = false;
                ErrorMessageElementName = rm.GetString("InvalidName");
                FontColorElementName = "Red";
                VisibilityErrorMessageElementName = true;
                return false;
            }
            EnableElement = true;
            ErrorMessageElementName = "";
            FontColorElementName = "Green";
            VisibilityErrorMessageElementName = false;
            return true;
        }

        #endregion

    }
}