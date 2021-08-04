using Accounting.dbSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote7308.SystemAdmin
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
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
                this.ltName.Text = dr["Name"].ToString();
                this.ltEmail.Text = dr["Email"].ToString();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            this.Session["UserLoginInfo"] = null;
            Response.Redirect("/Login.aspx");
        }
    }
}