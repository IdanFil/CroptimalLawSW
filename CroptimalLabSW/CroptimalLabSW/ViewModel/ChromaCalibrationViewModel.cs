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

namespace CroptimalLabSW.ViewModel
{
    public class ChromaCalibrationViewModel : ViewModelBase
    {
        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get
            {
                plotModel = new PlotModel();
                SetUpModel();
                return plotModel;
            }
            set
            {
                plotModel = value;
                RaisePropertyChanged("PlotModel");
            }
        }

        public ChromaCalibrationViewModel()
        {
            plotModel = new PlotModel();
            SetUpModel();
        }

        private void SetUpModel()
        {
            plotModel.TextColor = OxyColors.SteelBlue;
            var dateAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80, Title = "Abs", Position = AxisPosition.Bottom, AxislineColor = OxyColors.SteelBlue};
            plotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Concentration", Position = AxisPosition.Left, AxislineColor = OxyColors.SteelBlue };
            plotModel.Axes.Add(valueAxis);
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
