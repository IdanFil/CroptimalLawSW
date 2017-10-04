using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using CroptimalLabSW.Model.Chromameter;

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

        public bool insertChromaConfiguration(string i_confName, ObservableCollection<int> i_confParams)
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

        public bool insertChromaElement(string i_elementName, Calibration i_calibration)
        {
            string query = "INSERT INTO CroptimalDB.dbo.ChromameterElements (ElementName,Polynomial,CoefficientA,CoefficientB,CoefficientC,Regression,BackgroundReadingA,BackgroundReadingB,BackgroundReadingC,BackgroundReadingD,BackgroundReadingE,BackgroundReadingF,BackgroundReadingG,BackgroundReadingH,ConfigurationName,LedA,LedB,LedC,LedD,LedE,LedF,LedG,LedH)" +
                " VALUES ('" + i_elementName +
                "', '" + i_calibration.PolynomicOrder +
                "', '" + i_calibration.CoefficientA +
                "', '" + i_calibration.CoefficientB +
                "', '" + i_calibration.CoefficientC +
                "', '" + i_calibration.Regression +
                "', '" + i_calibration.BackgroundReading[0] +
                "', '" + i_calibration.BackgroundReading[1] +
                "', '" + i_calibration.BackgroundReading[2] +
                "', '" + i_calibration.BackgroundReading[3] +
                "', '" + i_calibration.BackgroundReading[4] +
                "', '" + i_calibration.BackgroundReading[5] +
                "', '" + i_calibration.BackgroundReading[6] +
                "', '" + i_calibration.BackgroundReading[7] +
                "', '" + i_calibration.ConfigurationName +
                "', '" + i_calibration.Configuration[0] +
                "', '" + i_calibration.Configuration[1] +
                "', '" + i_calibration.Configuration[2] +
                "', '" + i_calibration.Configuration[3] +
                "', '" + i_calibration.Configuration[4] +
                "', '" + i_calibration.Configuration[5] +
                "', '" + i_calibration.Configuration[6] +
                "', '" + i_calibration.Configuration[7] + "')";

            if (conn.ExecuteWriteCommand(query))
            {
                insertChromaMeasurements(i_elementName, i_calibration.Measurments);
                OnNewChromaConfigureAdded(new EventArgs());
                return true;
            }
            return false;
        }

        public bool insertChromaMeasurements(string i_elementName, ObservableCollection<Measurement> i_measurements)
        {
            for (int i = 0; i < i_measurements.Count; i++)
            {
                string query = "INSERT INTO CroptimalDB.dbo.ChromameterMeasurements (ElementName,Concentration,Absorption,Repetition,DetectorReadingA,DetectorReadingB,DetectorReadingC,DetectorReadingD,DetectorReadingE,DetectorReadingF,DetectorReadingG,DetectorReadingH)" +
                    " VALUES ('" + i_elementName +
                    "', '" + i_measurements[i].Concentration +
                    "', '" + i_measurements[i].Absorption +
                    "', '" + i_measurements[i].Repetition +
                    "', '" + i_measurements[i].DetectorReading[0] +
                    "', '" + i_measurements[i].DetectorReading[1] +
                    "', '" + i_measurements[i].DetectorReading[2] +
                    "', '" + i_measurements[i].DetectorReading[3] +
                    "', '" + i_measurements[i].DetectorReading[4] +
                    "', '" + i_measurements[i].DetectorReading[5] +
                    "', '" + i_measurements[i].DetectorReading[6] +
                    "', '" + i_measurements[i].DetectorReading[7] + "')";

                conn.ExecuteWriteCommand(query);
            }
            return true;
        }

        
        public bool updateChromaElement(string i_elementName, Calibration i_calibration)
        {
            string query = "UPDATE CroptimalDB.dbo.ChromameterElements SET " +
                "Polynomial='" + i_calibration.PolynomicOrder +
                "', CoefficientA='" + i_calibration.CoefficientA +
                "', CoefficientB='" + i_calibration.CoefficientB +
                "', CoefficientC='" + i_calibration.CoefficientC+
                "', Regression='" + i_calibration.Regression +
                "', BackgroundReadingA = '" + i_calibration.BackgroundReading[0] +
                "', BackgroundReadingB = '" + i_calibration.BackgroundReading[1] +
                "', BackgroundReadingC = '" + i_calibration.BackgroundReading[2] +
                "', BackgroundReadingD = '" + i_calibration.BackgroundReading[3] +
                "', BackgroundReadingE = '" + i_calibration.BackgroundReading[4] +
                "', BackgroundReadingF = '" + i_calibration.BackgroundReading[5] +
                "', BackgroundReadingG = '" + i_calibration.BackgroundReading[6] +
                "', BackgroundReadingH = '" + i_calibration.BackgroundReading[7] +
                "', ConfigurationName = '" + i_calibration.ConfigurationName +
                "', LedA = '" + i_calibration.Configuration[0] +
                "', LedB = '" + i_calibration.Configuration[1] +
                "', LedC = '" + i_calibration.Configuration[2] +
                "', LedD = '" + i_calibration.Configuration[3] +
                "', LedE = '" + i_calibration.Configuration[4] +
                "', LedF = '" + i_calibration.Configuration[5] +
                "', LedG = '" + i_calibration.Configuration[6] +
                "', LedH = '" + i_calibration.Configuration[7] +
                "' WHERE ElementName='" + i_elementName + "'";

            if (conn.ExecuteWriteCommand(query))
            {
                deleteChromaMeasureByElementName(i_elementName);
                insertChromaMeasurements(i_elementName, i_calibration.Measurments);
                return true;
            }
            return false; ;
        }

        public bool deleteChromaMeasureByElementName(string i_elementName)
        {
            string query = "DELETE FROM CroptimalDB.dbo.ChromameterMeasurements WHERE ElementName = '" + i_elementName + "'";
            return conn.ExecuteWriteCommand(query);
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
