using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CroptimalLabSW.Model.Chromameter;
using CroptimalLabSW.Model;

namespace CroptimalLabSW.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        MainWindowModel m_model;

        public MainWindowViewModel()
        {
            m_model = new MainWindowModel();
        }
        public RelayCommand WindowClosingCommand
        {
            get
            {
                return new RelayCommand(() => {
                    m_model.windowClosing();
                });
            }
        }
    }
}
