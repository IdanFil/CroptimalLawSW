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
        private ChromaCalibrationModel _chromaCalibrationModel;
        private ObservableCollection<string> _elementsList;

        #region Constructor
        public ChromaCalibrationViewModel()
        {
            _chromaCalibrationModel = new ChromaCalibrationModel();
            _chromaCalibrationModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
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
            get { return _chromaCalibrationModel.ElementsList; }
            set { _chromaCalibrationModel.ElementsList = value; }
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
            get { return _chromaCalibrationModel.SelectedElement; }
            set
            {
                _chromaCalibrationModel.SelectedElement = value;
                RaisePropertyChanged("SelectedElement");
            }
        }


        public string ElementName
        {
            get { return _chromaCalibrationModel.ElementName; }
            set
            {
                _chromaCalibrationModel.ElementName = value;
                RaisePropertyChanged("ElementName");
            }
        }


        public double Regression
        {
            get { return _chromaCalibrationModel.Regression; }
            set
            {
                _chromaCalibrationModel.Regression = value;
                RaisePropertyChanged("Regression");
            }
        }


        public int Polynomial
        {
            get { return _chromaCalibrationModel.Polynomial; }
            set
            {
                _chromaCalibrationModel.Polynomial = value;
                RaisePropertyChanged("Polynomial");
            }
        }


        public double Concentration
        {
            get { return _chromaCalibrationModel.Concentration; }
            set
            {
                _chromaCalibrationModel.Concentration = value;
                RaisePropertyChanged("Concentration");
            }
        }


        public int Repetitions
        {
            get { return _chromaCalibrationModel.Repetitions; }
            set
            {
                _chromaCalibrationModel.Repetitions = value;
                RaisePropertyChanged("Repetitions");
            }
        }


        public double BGReading
        {
            get { return _chromaCalibrationModel.BGReading; }
            set
            {
                _chromaCalibrationModel.BGReading = value;
                RaisePropertyChanged("BGReading");
            }
        }


        public int AVGNum
        {
            get { return _chromaCalibrationModel.AVGNum; }
            set
            {
                _chromaCalibrationModel.AVGNum = value;
                RaisePropertyChanged("AVGNum");
            }
        }

        public string Configuration
        {
            get { return _chromaCalibrationModel.Configuration; }
            set
            {
                _chromaCalibrationModel.Configuration = value;
                RaisePropertyChanged("Configuration");
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

        //public RelayCommand<string> ChangeDisplayCommand
        //{
        //    get
        //    {
        //        return new RelayCommand<string>(viewName => {
        //            Messenger.Default.Send<SwitchDisplayMessage>(new SwitchDisplayMessage { ViewName = viewName });
        //            Messenger.Default.Send<VisibilityBottomMenuMessage>(new VisibilityBottomMenuMessage { Visibility = true });
        //            Messenger.Default.Send<IsCheckedBottomMenuMessage>(new IsCheckedBottomMenuMessage { IsChecked = true });
        //        });
        //    }
        //}

        //public RelayCommand ExitCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(() => {
        //            Environment.Exit(0);
        //        });
        //    }
        //}
