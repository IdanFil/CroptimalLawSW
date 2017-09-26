using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CroptimalLabSW.Model
{
    class MainWindowModel
    {
        private DevicesController m_devicesController;

        public MainWindowModel()
        {
            m_devicesController = DevicesController.Instance();
        }

        public void windowClosing()
        {
            m_devicesController.closeAllDevices();
        }
    }
}
