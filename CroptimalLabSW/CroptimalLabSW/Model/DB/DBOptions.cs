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

        public Element getElementParams(string i_elementName)
        {
            string query = "SELECT ElementName,Polynomial,CoefficientA,CoefficientB,CoefficientC,Regression,BackgroundReadingA,BackgroundReadingB,BackgroundReadingC,BackgroundReadingD,BackgroundReadingE,BackgroundReadingF,BackgroundReadingG,BackgroundReadingH,ConfigurationName,LedA,LedB,LedC,LedD,LedE,LedF,LedG,LedH  FROM CroptimalDB.dbo.ChromameterElements WHERE ElementName='" + i_elementName + "'";
            DataTable DT = (DataTable)conn.ExecuteReadCommand(query);
            Element element = new Element();

            element.ElenemtName = i_elementName;
            element.PolynomicOrder = Convert.ToInt16(DT.Rows[0][1]);
            element.CoefficientA = Convert.ToDouble(DT.Rows[0][2]);
            element.CoefficientB = Convert.ToDouble(DT.Rows[0][3]);
            element.CoefficientC = Convert.ToDouble(DT.Rows[0][4]);
            element.Regression = Convert.ToDouble(DT.Rows[0][5]);
            element.BackgroundReading[0] = Convert.ToDouble(DT.Rows[0][6]);
            element.BackgroundReading[1] = Convert.ToDouble(DT.Rows[0][7]);
            element.BackgroundReading[2] = Convert.ToDouble(DT.Rows[0][8]);
            element.BackgroundReading[3] = Convert.ToDouble(DT.Rows[0][9]);
            element.BackgroundReading[4] = Convert.ToDouble(DT.Rows[0][10]);
            element.BackgroundReading[5] = Convert.ToDouble(DT.Rows[0][11]);
            element.BackgroundReading[6] = Convert.ToDouble(DT.Rows[0][12]);
            element.BackgroundReading[7] = Convert.ToDouble(DT.Rows[0][13]);
            element.ConfigurationName = Convert.ToString(DT.Rows[0][14]);
            element.Configuration[0] = Convert.ToInt16(DT.Rows[0][15]);
            element.Configuration[1] = Convert.ToInt16(DT.Rows[0][16]);
            element.Configuration[2] = Convert.ToInt16(DT.Rows[0][17]);
            element.Configuration[3] = Convert.ToInt16(DT.Rows[0][18]);
            element.Configuration[4] = Convert.ToInt16(DT.Rows[0][19]);
            element.Configuration[5] = Convert.ToInt16(DT.Rows[0][20]);
            element.Configuration[6] = Convert.ToInt16(DT.Rows[0][21]);
            element.Configuration[7] = Convert.ToInt16(DT.Rows[0][22]);

            return element;
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

        public bool insertChromaElement(string i_elementName, Element i_calibration)
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
                insertChromaCalibrationMeasurements(i_elementName, i_calibration.Measurments);
                OnNewChromaConfigureAdded(new EventArgs());
                return true;
            }
            return false;
        }

        public bool insertChromaCalibrationMeasurements(string i_elementName, ObservableCollection<Measurement> i_measurements)
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

        public bool insertChromaSampleMeasurements(string i_elementName, ObservableCollection<ChromaResult> i_measurements)
        {
            for (int i = 0; i < i_measurements.Count; i++)
            {
                string query = "INSERT INTO CroptimalDB.dbo.ChromameterResults (SampleID,ElementName,Concentration,Absorption,Repetition,DetectorReadingA,DetectorReadingB,DetectorReadingC,DetectorReadingD,DetectorReadingE,DetectorReadingF,DetectorReadingG,DetectorReadingH)" +
                    " VALUES ('" + i_measurements[i].SampleID +
                    "', '" + i_elementName +
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


        public bool updateChromaElement(string i_elementName, Element i_calibration)
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
                deleteChromaCalibrationMeasureByElementName(i_elementName);
                insertChromaCalibrationMeasurements(i_elementName, i_calibration.Measurments);
                return true;
            }
            return false; ;
        }

        public bool deleteChromaCalibrationMeasureByElementName(string i_elementName)
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
