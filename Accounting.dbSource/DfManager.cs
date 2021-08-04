using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.dbSource
{
    public class DfManager
    {
        public static DataTable GetStbkpg()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT top 1 * 
                  FROM Accounting
                  order by CreateDate asc
                 ";

            try
            {
                return ReadDataTable(ConnStr, dbCommand);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }

        public static DataTable GetEdbkpg()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT top 1 * 
                  FROM Accounting
                  order by CreateDate desc
                 ";

            try
            {
                return ReadDataTable(ConnStr, dbCommand);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }

        public static DataTable GetCount ()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT COUNT(Amount) as Amount
                  FROM Accounting
                 ";

            try
            {
                return ReadDataTable(ConnStr, dbCommand);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }

        public static DataTable GetMembers ()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT COUNT(Name) as Name
                  FROM UserInfo
                 ";

            try
            {
                return ReadDataTable(ConnStr, dbCommand);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }



        public static DataTable ReadDataTable(string connectionstring, string dbCommandstring)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(dbCommandstring, connection))
                {
                   
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();

                    return dt;
                }
            }
        }
    }
}
