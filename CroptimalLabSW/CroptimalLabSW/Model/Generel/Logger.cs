using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CroptimalLabSW.Model.Generel
{
    public class Logger
    {
        private static Logger s_Logger = null;
        private static object s_LockLogger = new object();
        private static StreamWriter m_LogsFile = null;
        string m_filePathLogs;

        public Logger()
        {
            m_filePathLogs = Path.Combine(Environment.CurrentDirectory, @"LogsFiles\LogFile.txt");
            m_LogsFile = new StreamWriter(m_filePathLogs, true);
            m_LogsFile.AutoFlush = true;
            m_LogsFile.Close();
        }

        public static Logger Instance()
        {
            lock (s_LockLogger)
            {
                if (s_Logger == null)
                {
                    s_Logger = new Logger();
                }
            }
            return s_Logger;
        }

        public void WriteLog(string i_sender, string i_massege)
        {
            lock (s_LockLogger)
            {
                m_LogsFile = new StreamWriter(m_filePathLogs, true);
                m_LogsFile.WriteLine(new Log(i_sender, i_massege));
                m_LogsFile.Close();
            }
        }

        public void CloseFile()
        {
            m_LogsFile.Close();
        }

        ~Logger()
        {
            CloseFile();
        }
    }
}
