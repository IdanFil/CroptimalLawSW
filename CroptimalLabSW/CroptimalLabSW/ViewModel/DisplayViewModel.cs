using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using CroptimalLabSW.Messages;

namespace CroptimalLabSW.ViewModel
{
    public class DisplayViewModel : ViewModelBase
    {
        private Boolean _menuVisibility;

        public Boolean MenuVisibility
        {
            get { return _menuVisibility; }
            set
            {
                _menuVisibility = value;
                RaisePropertyChanged("MenuVisibility");
            }
        }

        private string _contentControlView;
        public string ContentControlView
        {
            get { return _contentControlView; }
            set
            {
                _contentControlView = value;
                RaisePropertyChanged("ContentControlView");
            }
        }

        public DisplayViewModel()
        {
            Messenger.Default.Register<SwitchDisplayMessage>(this, (switchDisplayMessage) =>
            {
                ContentControlView = switchDisplayMessage.ViewName;
            });
             ContentControlView = "MenuView";

            Messenger.Default.Register<VisibilityBottomMenuMessage>(this, (visibilityBottomMenuMessage) =>
            {
                MenuVisibility = visibilityBottomMenuMessage.Visibility;
            });
        }
    }
}
