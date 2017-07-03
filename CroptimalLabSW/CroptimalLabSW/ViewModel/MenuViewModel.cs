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

namespace CroptimalLabSW.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {

        public RelayCommand OpenNiarPageCommand { get; private set; }

        public MenuViewModel()
        {
        }

        public RelayCommand<string> ChangeDisplayCommand
        {
            get
            {
                return new RelayCommand<string>(viewName => {
                    Messenger.Default.Send<SwitchDisplayMessage>(new SwitchDisplayMessage { ViewName = viewName });
                    Messenger.Default.Send<VisibilityBottomMenuMessage>(new VisibilityBottomMenuMessage { Visibility = true });
                    Messenger.Default.Send<IsCheckedBottomMenuMessage>(new IsCheckedBottomMenuMessage { IsChecked = true });
                });
            }
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
    }
}
