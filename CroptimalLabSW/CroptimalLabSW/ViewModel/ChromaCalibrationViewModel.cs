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

namespace CroptimalLabSW.ViewModel
{
    public class ChromaCalibrationViewModel : ViewModelBase
    {

        private PlotModel _plotModel;
        private ChromaCalibrationModel m_chromaCalibrationModel;

        #region Constructor
        public ChromaCalibrationViewModel()
        {
            m_chromaCalibrationModel = new ChromaCalibrationModel();
            m_chromaCalibrationModel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                RaisePropertyChanged(e.PropertyName);
            };
            _plotModel = new PlotModel();
            SetUpModel();
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
                _plotModel = new PlotModel();
                SetUpModel();
                return _plotModel;
            }
            set
            {
                _plotModel = value;
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


        public string ElementName
        {
            get { return m_chromaCalibrationModel.ElementName; }
            set
            {
                m_chromaCalibrationModel.ElementName = value;
                RaisePropertyChanged("ElementName");
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


        public int Polynomial
        {
            get { return m_chromaCalibrationModel.Polynomial; }
            set
            {
                m_chromaCalibrationModel.Polynomial = value;
                RaisePropertyChanged("Polynomial");
            }
        }


        public double Concentration
        {
            get { return m_chromaCalibrationModel.Concentration; }
            set
            {
                m_chromaCalibrationModel.Concentration = value;
                RaisePropertyChanged("Concentration");
            }
        }


        public int Repetitions
        {
            get { return m_chromaCalibrationModel.Repetitions; }
            set
            {
                m_chromaCalibrationModel.Repetitions = value;
                RaisePropertyChanged("Repetitions");
            }
        }


        public double BGReading
        {
            get { return m_chromaCalibrationModel.BGReading; }
            set
            {
                m_chromaCalibrationModel.BGReading = value;
                RaisePropertyChanged("BGReading");
            }
        }


        public int AVGNum
        {
            get { return m_chromaCalibrationModel.AVGNum; }
            set
            {
                m_chromaCalibrationModel.AVGNum = value;
                RaisePropertyChanged("AVGNum");
            }
        }

        public string SelectedConfiguration
        {
            get { return m_chromaCalibrationModel.SelectedConfiguration; }
            set
            {
                m_chromaCalibrationModel.SelectedConfiguration = value;
                RaisePropertyChanged("SelectedConfiguration");
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

        #endregion

        private void SetUpModel()
        {
            _plotModel.TextColor = OxyColors.SteelBlue;
            var dateAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80, Title = "Abs", Position = AxisPosition.Bottom, AxislineColor = OxyColors.SteelBlue};
            _plotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Concentration", Position = AxisPosition.Left, AxislineColor = OxyColors.SteelBlue };
            _plotModel.Axes.Add(valueAxis);
        }

    }
}