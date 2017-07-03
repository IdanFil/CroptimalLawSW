using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using CroptimalLabSW.Messages;
using System.Collections.ObjectModel;

namespace CroptimalLabSW.ViewModel
{
    public class BottomMenuViewModel : ViewModelBase
    {
        private bool _chromameterIsChecked;

        public bool ChromameterIsChecked
        {
            get { return _chromameterIsChecked; }
            set
            {
                _chromameterIsChecked = value;
                RaisePropertyChanged("ChromameterIsChecked");
            }
        }


        public RelayCommand<string> ChangeDisplayCommand
        {
            get
            {
                return new RelayCommand<string>(viewName => {
                    Messenger.Default.Send<SwitchDisplayMessage>(new SwitchDisplayMessage { ViewName = viewName });
                    if(viewName == "MenuView")
                    {
                        Messenger.Default.Send<VisibilityBottomMenuMessage>(new VisibilityBottomMenuMessage { Visibility = false });
                    }
                    //else
                    //{
                    //    Messenger.Default.Send<VisibilityBottomMenuMessage>(new VisibilityBottomMenuMessage { Visibility = true });
                    //}
                });
            }
        }

        public BottomMenuViewModel()
        {
            Messenger.Default.Register<IsCheckedBottomMenuMessage>(this, (isCheckedBottomMenuMessage) =>
            {
                ChromameterIsChecked = isCheckedBottomMenuMessage.IsChecked;
            });
        }

        public RelayCommand ExitCommand
        {
            get
            {
                return new RelayCommand(() => {
                    Environment.Exit(0);
                });
            }
        }

        //public RelayCommand<string> ChangeDisplayCommand
        //{
        //    get
        //    {
        //        return new RelayCommand<string>(viewName => sendViewNameToShow(viewName));
        //    }
        //}

        //public void sendViewNameToShow(string i_viewName)
        //{
        //    Messenger.Default.Send<SwitchDisplayMessage>(new SwitchDisplayMessage { ViewName = i_viewName });
        //}

    }
}
