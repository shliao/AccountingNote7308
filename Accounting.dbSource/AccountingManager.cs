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
                  FROM AccountingNote
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
                  FROM AccountingNote
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
                  FROM AccountingNote
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
        public static DataTable GetIncome(string userid)
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT SUM (Amount) AS 'AT'
                  FROM  [AccountingNote]
                  WHERE [ActType] = 1
                  AND   [UserID] = @userid
                 ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userid", userid));

            try
            {
                return dbHelper.ReadDataTable(ConnStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static DataTable GetExpenses(string userid)
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT SUM (Amount) AS 'ATS'
                  FROM  [AccountingNote]
                  WHERE [ActType] = 0
                  AND   [UserID] = @userid
                 ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userid", userid));

            try
            {
                return dbHelper.ReadDataTable(ConnStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static DataTable GetAccountingList(string userID)
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT 
                     ID, 
                     Caption,
                     Amount,
                     ActType,
                     CreateDate
                  FROM AccountingNote
                  WHERE  [UserID] = @userid
                  ORDER BY CreateDate DESC
                 ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", userID));


            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    try
                    {
                        return dbHelper.ReadDataTable(ConnStr, dbCommand, list);
                    }
                    catch (Exception ex)
                    {
                        Logger.Writelog(ex);
                        return null;
                    }
                }

            }
        }
        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            //值的驗證
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000 .");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("actType must be 0 or 1.");

            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                $@" INSERT INTO [dbo].[AccountingNote]
                   (
                      UserID
                     ,Caption
                     ,Amount
                     ,ActType
                     ,CreateDate
                     ,Body
                   )
                   VALUES
                   (
                     @userID
                     ,@caption
                     ,@amount
                     ,@actType
                     ,@createDate
                     ,@body
                   )
                 ";


            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@userID", userID);
                    comm.Parameters.AddWithValue("@caption", caption);
                    comm.Parameters.AddWithValue("@amount", amount);
                    comm.Parameters.AddWithValue("@actType", actType);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now);
                    comm.Parameters.AddWithValue("@body", body);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.Writelog(ex);
                    }
                }

            }
        }
        public static bool UpdaateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        {

            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000 .");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1.");

            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                $@" UPDATE [AccountingNote]
                   SET
                      UserID     =@userID
                     ,Caption    =@caption
                     ,Amount     =@amount
                     ,ActType    =@actType
                     ,CreateDate =createDate
                     ,Body       =@body
                   WHERE
                       ID=@id";


            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@userID", userID);
                    comm.Parameters.AddWithValue("@caption", caption);
                    comm.Parameters.AddWithValue("@amount", amount);
                    comm.Parameters.AddWithValue("@actType", actType);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now);
                    comm.Parameters.AddWithValue("@body", body);
                    comm.Parameters.AddWithValue("@id", ID);

                    try
                    {
                        conn.Open();
                        int effectRows = comm.ExecuteNonQuery();

                        if (effectRows == 1)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        Logger.Writelog(ex);
                        return false;
                    }
                }

            }

        }
        public static void DeleteAccounting(int ID)
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                $@" DELETE [AccountingNote]
                  
                   WHERE ID= @id";



            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {

                    comm.Parameters.AddWithValue("@id", ID);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.Writelog(ex);
                    }
                }
            }
        }
        public static DataRow GetAccounting(int id, string userID)
        {

            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                $@"SELECT 
                     ID, 
                     Caption,
                     Amount,
                     ActType,
                     CreateDate,
                     Body
                   FROM AccountingNote
                   WHERE id = @id AND UserID = @userID";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@id", id);
                    comm.Parameters.AddWithValue("@userID", userID);

                    try
                    {
                        conn.Open();
                        var reader = comm.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        if (dt.Rows.Count == 0)
                            return null;
                        return dt.Rows[0];
                    }
                    catch (Exception ex)
                    {
                        Logger.Writelog(ex);
                        return null;

                    }
                }

            }

        }

    }
}
