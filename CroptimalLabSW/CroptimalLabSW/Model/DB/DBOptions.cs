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
        private DBConnection conn;

        public DBOptions()
        {
            conn = DBConnection.Instance;
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

        public ObservableCollection<bool> getConfiguretion(string i_configureName)
        {
            string query = "SELECT LedA,LedB,LedC,LedD,LedE,LedF,LedG,LedH,DetectorA,DetectorB,DetectorC,DetectorD,DetectorE,DetectorF,DetectorG,DetectorH FROM CroptimalDB.dbo.ChromameterConfigurations WHERE ConfigurationName='" + i_configureName + "'";
            DataTable DT = (DataTable)conn.ExecuteReadCommand(query);
            ObservableCollection<bool> configure = new ObservableCollection<bool>();
            for (int i = 0; i < DT.Columns.Count; i++)
            {
                configure.Add(Convert.ToBoolean(DT.Rows[0][i]));
            }
            return configure;
        }

        public bool insertNewChromaConfiguration(string i_confName, ObservableCollection<bool> i_confParams)
        {
            string query = "INSERT INTO CroptimalDB.dbo.ChromameterConfigurations (ConfigurationName,LedA,LedB,LedC,LedD,LedE,LedF,LedG,LedH,DetectorA,DetectorB,DetectorC,DetectorD,DetectorE,DetectorF,DetectorG,DetectorH)" +
                " VALUES ('" + i_confName +
                "', '" + i_confParams[0] +
                "', '" + i_confParams[1] +
                "', '" + i_confParams[2] +
                "', '" + i_confParams[3] +
                "', '" + i_confParams[4] +
                "', '" + i_confParams[5] +
                "', '" + i_confParams[6] +
                "', '" + i_confParams[7] +
                "', '" + i_confParams[8] +
                "', '" + i_confParams[9] +
                "', '" + i_confParams[10] +
                "', '" + i_confParams[11] +
                "', '" + i_confParams[12] +
                "', '" + i_confParams[13] +
                "', '" + i_confParams[14] +
                "', '" + i_confParams[15] + "')";
            
            return conn.ExecuteWriteCommand(query);
        }


        public bool updateChromaConfiguration(string i_selectedConfName, ObservableCollection<bool> i_confParams)
        {
            string query = "UPDATE CroptimalDB.dbo.ChromameterConfigurations SET LedA='" + i_confParams[0] + "', LedB='" + i_confParams[1] + "', LedC='" + i_confParams[2]
                + "', LedD='" + i_confParams[3] + "', LedE='" + i_confParams[4] + "', LedF = '" + i_confParams[5] + "', LedG = '" + i_confParams[6] + "', LedH = '" + i_confParams[7]
                + "', DetectorA = '" + i_confParams[8] + "', DetectorB = '" + i_confParams[9] + "', DetectorC = '" + i_confParams[10] + "', DetectorD = '" + i_confParams[11]
                 + "', DetectorE = '" + i_confParams[12] + "', DetectorF = '" + i_confParams[13] + "', DetectorG = '" + i_confParams[14] + "', DetectorH = '" + i_confParams[15] 
                 + "' WHERE ConfigurationName='" + i_selectedConfName + "'";

            return conn.ExecuteWriteCommand(query);
        }
        #endregion
    }
}
