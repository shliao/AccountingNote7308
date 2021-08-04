﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.dbSource
{
    public class AccountingManager
    {
        
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
                   FROM Accounting
                   WHERE UserID = @userID
                 ";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))   
                {
                    comm.Parameters.AddWithValue("@userID", userID);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = comm.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        return dt;
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
                $@" INSERT INTO [dbo].[Accounting]
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
                $@" UPDATE [Accounting]
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
                $@" DELETE [Accounting]
                  
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
                   FROM Accounting
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
