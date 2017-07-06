using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CroptimalLabSW.Model.Generel;

namespace CroptimalLabSW.Model.DB
{
    public sealed class DBConnection
    {
        private const string c_className = "DB Connection";
        private const string DB_Name = "CroptimalDB";
        private static volatile DBConnection instance;
        private static object syncRoot = new Object();
        private Logger logger = Logger.Instance();

        private string m_sConnectionString;
        private SqlConnection m_SqlConnection = null;

        private DBConnection()
        {
            m_sConnectionString = @"Data Source=" + Environment.MachineName + @"\MSSQL;Initial Catalog=" + DB_Name + @";Integrated Security=True;Pooling=False;";
        }

        public static DBConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DBConnection();
                        }
                    }
                }

                return instance;
            }
        }

        private bool Connect()
        {
            try
            {
                if (m_SqlConnection == null)
                {
                    m_SqlConnection = new SqlConnection(m_sConnectionString);
                    m_SqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(c_className, "Connect failed");
                logger.WriteLog(c_className, "Exception-" + ex.Message);
                return false;
            }
            return true;
        }

        private bool Disonnect()
        {
            try
            {
                if (m_SqlConnection != null)
                {
                    m_SqlConnection.Close();
                    m_SqlConnection = null;
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(c_className, "Disconnect failed");
                logger.WriteLog(c_className, "Exception-" + ex.Message);
                return false;
            }
            return true;
        }

        public object ExecuteReadCommand(string sCommand)
        {
            object oResult = null;
            try
            {
                Connect();
                if ((m_SqlConnection != null) && (m_SqlConnection.State == System.Data.ConnectionState.Open))
                {
                    using (SqlCommand cmd = new SqlCommand(sCommand, m_SqlConnection))
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(cmd.ExecuteReader());
                        oResult = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(c_className, "Execute read command failed");
                logger.WriteLog(c_className, "Exception-" + ex.Message);
            }
            finally
            {
                Disonnect();
            }
            return oResult;
        }

        public object ExecuteWriteCommand(string sCommand)
        {
            object oResult = null;
            try
            {
                Connect();
                if ((m_SqlConnection != null) && (m_SqlConnection.State == System.Data.ConnectionState.Open))
                {
                    using (SqlCommand cmd = new SqlCommand(sCommand, m_SqlConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(c_className, "Execute write command failed");
                logger.WriteLog(c_className, "Exception-" + ex.Message);
            }
            finally
            {
                Disonnect();
            }
            return oResult;
        }
    }
}
