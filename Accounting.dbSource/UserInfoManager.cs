using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Accounting.dbSource
{
    public class UserInfoManager
    {
        public static DataRow GetUserInfobyAccount(string account)
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"SELECT [ID], [Account], [PWD],
                                              [Name], [Email], [UserLevel],
                                              [CreateDate]
                                       FROM   [UserInfo]
                                       WHERE  [Account] = @account";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@account", account));

            try
            {
                return dbHelper.ReadDataRow(connectionstring, dbCommandstring, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static DataRow GetUserInfobyUID(string uid)
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"SELECT [ID], [Account], [PWD],
                                              [Name], [Email], [UserLevel],
                                              [CreateDate]
                                       FROM   [UserInfo]
                                       WHERE  [ID] = @uid";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@uid", uid));

            try
            {
                return dbHelper.ReadDataRow(connectionstring, dbCommandstring, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static DataTable GetUserInfoList_Order()
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"SELECT [Account], [Name], [Email], [UserLevel],
                                              [CreateDate], [ID]
                                       FROM   [UserInfo]
                                       ORDER BY [CreateDate] DESC";

            List<SqlParameter> list = new List<SqlParameter>();

            try
            {
                return dbHelper.ReadDataTable(connectionstring, dbCommandstring, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static DataTable GetAllAccount()
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"SELECT [Account] FROM [UserInfo]";

            try
            {
                return dbHelper.GetDataTable(connectionstring, dbCommandstring);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static void DeleteUser(string uid)
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"DELETE FROM [UserInfo]
                                       WHERE       [ID] = @uid";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@uid", uid));

            try
            {
                int effectRowsCnt = dbHelper.ModifyData(connectionstring, dbCommandstring, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
            }
        }
        public static void CreateUser(string account, string name, string email, int userlevel, string pwd)
        {
            if (userlevel != 0 && userlevel != 1)
                throw new ArgumentException("必須是管理者或一般會員.");

            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"INSERT INTO [UserInfo]
                                                  ([Account], [Name], [PWD],
                                                   [Email], [UserLevel], [CreateDate])
                                       VALUES     (@account, @name, @pwd,
                                                   @email, @userlevel, @date)";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@Account", account));
            list.Add(new SqlParameter("@name", name));
            list.Add(new SqlParameter("@email", email));
            list.Add(new SqlParameter("@userlevel", userlevel));
            list.Add(new SqlParameter("@date", DateTime.Now));
            list.Add(new SqlParameter("@pwd", pwd));

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(dbCommandstring, connection))
                {
                    try
                    {
                        dbHelper.CreateData(connectionstring, dbCommandstring, list);
                    }
                    catch (Exception ex)
                    {
                        Logger.Writelog(ex);
                    }
                }
            }
        }
        public static bool UpdateUser(string uid, string name, string email)
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"UPDATE [UserInfo]
                                       SET    [Name] = @name,
                                              [Email] = @email,
                                              [CreateDate] = @date
                                       WHERE  [ID] = @uid";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@uid", uid));
            list.Add(new SqlParameter("@name", name));
            list.Add(new SqlParameter("@email", email));
            list.Add(new SqlParameter("@date", DateTime.Now));

            try
            {
                int effectRowsCnt = dbHelper.ModifyData(connectionstring, dbCommandstring, list);

                if (effectRowsCnt == 1)
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
        public static bool UpdatePWD(string Account, string PWD)
        {
            string connStr = dbHelper.Getconnectionstring();
            string dbCommand =
                $@"UPDATE [UserInfo]
                   SET
                      PWD = @pwd
                  WHERE
                      Account = @account
                ";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@account", Account);
                    comm.Parameters.AddWithValue("@pwd", PWD);

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
        public static List<string> GetAllAccountList()
        {
            DataTable dt = GetAllAccount();

            List<string> acclist = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    acclist.Add(dr[dc].ToString());
                }
            }
            return acclist;
        }
        public static DataTable GetUser()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT COUNT(Name) as Name
                  FROM UserInfo
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
