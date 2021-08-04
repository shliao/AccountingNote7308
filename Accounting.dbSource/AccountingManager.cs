using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.dbSource
{
    public class AccountingManager
    {
        public static DataTable GetStartData()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT top 1 * 
                  FROM Accounting
                  order by CreateDate asc
                 ";

            try
            {
                return dbHelper.GetDataTable(ConnStr, dbCommand);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static DataTable GetEndData()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT top 1 * 
                  FROM Accounting
                  order by CreateDate desc
                 ";

            try
            {
                return dbHelper.GetDataTable(ConnStr, dbCommand);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static DataTable GetDataCount()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT COUNT(Amount) as Amount
                  FROM Accounting
                 ";

            try
            {
                return dbHelper.GetDataTable(ConnStr, dbCommand);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
    }
}
