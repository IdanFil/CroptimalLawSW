using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CroptimalLabSW.Model.Generel
{
    class Log
    {
        public string m_date { get; set; }
        public string m_time { get; set; }
        public string m_logType { get; set; }
        public string m_message { get; set; }

        public Log(string i_message)
        {
            m_date = DateTime.Now.ToString("dd/MM/yyyy");
            m_time = DateTime.Now.ToString("HH:mm:ss");
            m_message = i_message;
            m_logType = null;
        }

        public Log(string i_logType, string i_message)
        {
            m_date = DateTime.Now.ToString("dd/MM/yyyy");
            m_time = DateTime.Now.ToString("HH:mm:ss");
            m_message = i_message;
            m_logType = i_logType;
        }

        public override string ToString()
        {
            if (m_logType == null)
            {
                return m_date + "\t" + m_time + "\t" + m_message;
            }
            return m_date + "\t" + m_time + "\t" + m_logType + ":\t" + m_message;
        }
    }
}
