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
        private bool _wormUpEnabled;
        private string _errorMessageElementName;
        private bool _visibilityErrorMessageElementName;
        private string _fontColorElementName;
        private bool _enableAddNewElement;
        
        ResourceManager rm;

        #region Constructor
        public ChromaCalibrationViewModel()
        {
            dialogCoordinator = DialogCoordinator.Instance;
            m_chromaCalibrationModel = new ChromaCalibrationModel();
            rm = new ResourceManager("CroptimalLabSW.Resources.Strings", Assembly.GetExecutingAssembly());
            WormUpEnabled = false;
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
            set { m_chromaCalibrationModel.ElementsList = value; }
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
                RaisePropertyChanged("SelectedElement");
            }
        }

        public bool WormUpEnabled
        {
            get { return _wormUpEnabled; }
            set
            {
                _wormUpEnabled = value;
                RaisePropertyChanged("WormUpEnabled");
            }
        }
        


        public string NewElementName
        {
            get { return m_chromaCalibrationModel.NewElementName; }
            set
            {
                m_chromaCalibrationModel.NewElementName = value;
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

        public bool EnableAddNewElement
        {
            get { return _enableAddNewElement; }
            set
            {
                _enableAddNewElement = value;
                RaisePropertyChanged("EnableAddNewElement");
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

        #endregion

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
                if (_wormUpEnabled)
                {
                    //wait for warmup
                }
            }
        }

        private bool checkNewConfNameValidation()
        {
            if (NewElementName.Equals(""))
            {
                EnableAddNewElement = false;
                ErrorMessageElementName = "";
                FontColorElementName = "Black";
                VisibilityErrorMessageElementName = true;
                return false;
            }
            if (ConfNamesList.Contains(NewElementName))
            {
                //name exist
                EnableAddNewElement = false;
                ErrorMessageElementName = rm.GetString("ExistsName");
                FontColorElementName = "Red";
                VisibilityErrorMessageElementName = true;
                return false;
            }
            if (NewElementName.Length == 1)
            {
                //short
                EnableAddNewElement = false;
                ErrorMessageElementName = rm.GetString("ShortName");
                FontColorElementName = "Red";
                VisibilityErrorMessageElementName = true;
                return false;
            }
            if (!((NewElementName[0] >= 65 && NewElementName[0] <= 91) || (NewElementName[0] >= 97 && NewElementName[0] <= 122)))
            {
                //Invalid name
                EnableAddNewElement = false;
                ErrorMessageElementName = rm.GetString("InvalidName");
                FontColorElementName = "Red";
                VisibilityErrorMessageElementName = true;
                return false;
            }
            EnableAddNewElement = true;
            ErrorMessageElementName = "";
            FontColorElementName = "Green";
            VisibilityErrorMessageElementName = false;
            return true;
        }



    }
}