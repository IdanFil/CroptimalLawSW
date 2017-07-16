using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;

namespace CroptimalLabSW.Model.DB
{
    class DBOptions
    {
        private static DBOptions s_DBOptions = null;
        private static object s_LockDBOptions = new object();

        public event EventHandler<EventArgs> NewChromaConfigureAdded;
        private DBConnection conn;

        private DBOptions()
        {
            conn = DBConnection.Instance;
        }

        public static DBOptions Instance()
        {
            lock (s_LockDBOptions)
            {
                if (s_DBOptions == null)
                {
                    s_DBOptions = new DBOptions();
                }
            }
            return s_DBOptions;
        }

        #region Chromameter

        public ObservableCollection<string> getChromameterElementsName()
        {
            string query = "SELECT ElementName FROM CroptimalDB.dbo.ChromameterElements";
            DataTable DT = (DataTable)conn.ExecuteReadCommand(query);
            ObservableCollection<string> elementsList = new ObservableCollection<string>();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                elementsList.Add((string)DT.Rows[i][0]);
            }
            return elementsList;
        }

        
        public ObservableCollection<string> getChromameterConfigurationNames()
        {
            string query = "SELECT ConfigurationName FROM CroptimalDB.dbo.ChromameterConfigurations";
            DataTable DT = (DataTable)conn.ExecuteReadCommand(query);
            ObservableCollection<string> configureList = new ObservableCollection<string>();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                configureList.Add((string)DT.Rows[i][0]);
            }
            return configureList;
        }

        public ObservableCollection<int> getConfiguretion(string i_configureName)
        {
            string query = "SELECT LedA,LedB,LedC,LedD,LedE,LedF,LedG,LedH FROM CroptimalDB.dbo.ChromameterConfigurations WHERE ConfigurationName='" + i_configureName + "'";
            DataTable DT = (DataTable)conn.ExecuteReadCommand(query);
            ObservableCollection<int> configure = new ObservableCollection<int>();
            for (int i = 0; i < DT.Columns.Count; i++)
            {
                configure.Add(Convert.ToInt16(DT.Rows[0][i]));
            }
            return configure;
        }

        public bool insertNewChromaConfiguration(string i_confName, ObservableCollection<int> i_confParams)
        {
            string query = "INSERT INTO CroptimalDB.dbo.ChromameterConfigurations (ConfigurationName,LedA,LedB,LedC,LedD,LedE,LedF,LedG,LedH)" +
                " VALUES ('" + i_confName +
                "', '" + i_confParams[0] +
                "', '" + i_confParams[1] +
                "', '" + i_confParams[2] +
                "', '" + i_confParams[3] +
                "', '" + i_confParams[4] +
                "', '" + i_confParams[5] +
                "', '" + i_confParams[6] +
                "', '" + i_confParams[7] + "')";
            
            if(conn.ExecuteWriteCommand(query))
            {
                OnNewChromaConfigureAdded(new EventArgs());
                return true;
            }
            return false;
        }


        public bool updateChromaConfiguration(string i_selectedConfName, ObservableCollection<int> i_confParams)
        {
            string query = "UPDATE CroptimalDB.dbo.ChromameterConfigurations SET LedA='" + i_confParams[0] + "', LedB='" + i_confParams[1] + "', LedC='" + i_confParams[2]
                + "', LedD='" + i_confParams[3] + "', LedE='" + i_confParams[4] + "', LedF = '" + i_confParams[5] + "', LedG = '" + i_confParams[6] + "', LedH = '" + i_confParams[7] 
                 + "' WHERE ConfigurationName='" + i_selectedConfName + "'";

            return conn.ExecuteWriteCommand(query);
        }

        public bool updateChromaWarmUpSec(int i_warmUpSec)
        {
            string query = "UPDATE CroptimalDB.dbo.ChromameterSettings SET WarmUpSec ='" + i_warmUpSec + "'";
            return conn.ExecuteWriteCommand(query);
        }

        public int getChromameterWarmUpSec()
        {
            string query = "SELECT WarmUpSec FROM CroptimalDB.dbo.ChromameterSettings";
            DataTable DT = (DataTable)conn.ExecuteReadCommand(query);
            return (int)DT.Rows[0][0];
        }

        public int getChromameterQuantityOfLEDs()
        {
            string query = "SELECT QuantityOfLEDs FROM CroptimalDB.dbo.ChromameterSettings";
            DataTable DT = (DataTable)conn.ExecuteReadCommand(query);
            return (int)DT.Rows[0][0];
        }

        private void OnNewChromaConfigureAdded(EventArgs e)
        {
            EventHandler<EventArgs> handler = NewChromaConfigureAdded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }
}
