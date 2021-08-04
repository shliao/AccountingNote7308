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
using AccountingNote;

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

            if (string.Compare(dbPWD, iptPWD, true) == 0)
            {
                this.txbPWD.Text = dr["PWD"].ToString();

                string iptNewPWD = txbNewPWD.Text;
                string iptNewPWD_Check = txbNewPWD_Check.Text;

                if (string.Compare(iptNewPWD, iptNewPWD_Check, true) == 0)
                {
                    UserInfoManager.UpdatePWD(dr["Account"].ToString(), iptNewPWD_Check);
                    this.Session["UserLoginInfo"] = null;
                    Response.Redirect("/Login.aspx");
                    return;
                }
            }
            else
            {
                this.txbPWD.Text = string.Empty;
                this.txbNewPWD.Text = string.Empty;
                this.txbNewPWD_Check.Text = string.Empty;
                Response.Redirect("UserPassword.aspx");
                return;
            }
        }
    }
}