using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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

        public List<string> getChromameterElementsName()
        {
            string query = "SELECT ElementName FROM CroptimalDB.dbo.ChromameterElements";
            DataTable DT = (DataTable)conn.ExecuteReadCommand(query);
            List<string> elementsList = new List<string>();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                elementsList.Add((string)DT.Rows[i][0]);
            }
            return elementsList;
        }

        #endregion
    }
}
