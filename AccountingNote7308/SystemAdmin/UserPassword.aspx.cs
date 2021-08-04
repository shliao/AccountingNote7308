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

            string dbPWD = dr["PWD"].ToString();
            string iptPWD = txbPWD.Text;

            if (dbPWD == iptPWD)
            {
                this.txbPWD.Text = dr["PWD"].ToString();
            }
            else
            {
                this.txbPWD.Text = string.Empty;
                Response.Redirect("UserPassword.aspx");
                return;
            }

            string iptNewPWD = txbNewPWD.Text;
            string iptNewPWD_Check = txbNewPWD_Check.Text;

            if (iptNewPWD == iptNewPWD_Check)
            {
                UserInfoManager.UpdatePWD(dr["Account"].ToString(), iptNewPWD_Check);
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
    }
}