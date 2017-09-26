using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CroptimalLabSW.Interfaces
{
    interface IdeviceManager
    {
        bool close();
        bool open();
    }
}
