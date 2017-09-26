using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CroptimalLabSW.Interfaces;

namespace CroptimalLabSW.Model
{
    class DevicesController
    {
        private static DevicesController s_devicesController = null;
        private static object s_Locker = new object();

        public event EventHandler<EventArgs> CloseDevices;

        List<IdeviceManager> m_devicesList;

        private DevicesController()
        {
            m_devicesList = new List<IdeviceManager>();
        }

        public static DevicesController Instance()
        {
            lock (s_Locker)
            {
                if (s_devicesController == null)
                {
                    s_devicesController = new DevicesController();
                }
            }
            return s_devicesController;
        }

        public void addToDevicesList(IdeviceManager i_device)
        {
            m_devicesList.Add(i_device);
        }

        public void closeAllDevices()
        {
            OnCloseDevices(new EventArgs());
        }

        private void OnCloseDevices(EventArgs e)
        {
            EventHandler<EventArgs> handler = CloseDevices;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
