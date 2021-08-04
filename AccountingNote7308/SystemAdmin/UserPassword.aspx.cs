using Accounting.dbSource;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote7308.SystemAdmin
{
    public partial class UserPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["UserLoginInfo"] == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string account = this.Session["UserLoginInfo"] as string;
            DataRow dr = UserInfoManager.GetUserInfobyAccount(account);

            if (dr == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            this.ltAccount.Text = dr["Account"].ToString();
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            string account = this.Session["UserLoginInfo"] as string;
            DataRow dr = UserInfoManager.GetUserInfobyAccount(account);

            if (dr == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            this.ltAccount.Text = dr["Account"].ToString();

            string a = dr["PWD"].ToString();
            string b = txbPWD.Text;

            if (a == b)
            {
                this.txbPWD.Text = dr["PWD"].ToString();
            }
            else
            {
                this.txbPWD.Text = string.Empty;
                Response.Redirect("UserPassword.aspx");
                return;
            }

            string c = txbNewPWD.Text;
            string d = txbNewPWD_Check.Text;

            if (c == d)
            {
                UpdatePWD(dr["Account"].ToString(), d);
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }
            else
            {
                this.txbNewPWD.Text = string.Empty;
                this.txbNewPWD_Check.Text = string.Empty;
                Response.Redirect("UserPassword.aspx");
                return;
            }
        }

        public static bool UpdatePWD(string Account, string PWD)
        {
            string connStr = GetConnectionString2();
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

        public static string GetConnectionString2()
        {
            //string val = ConfigurationManager.AppSettings["ConnectionString"];
            string val2 = ConfigurationManager.ConnectionStrings["Default Connection"].ConnectionString;
            return val2;
        }
    }
}