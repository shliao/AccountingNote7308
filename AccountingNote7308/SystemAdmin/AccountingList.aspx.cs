using Accounting.dbSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote7308.SystemAdmin
{
    public partial class AccountingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string account = this.Session["UserLoginInfo"] as string;
            var dr = UserInfoManager.GetUserInfobyAccount(account);

            if (dr != null)
            {
                if (this.Session["UserLoginInfo"] == null)
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                var dt = AccountingManager.GetAccountingList(dr["ID"].ToString());

                if (dt.Rows.Count > 0)                   
                {
                    this.gvAccountingList.DataSource = dt;           
                    this.gvAccountingList.DataBind();
                }
                else
                {
                    this.gvAccountingList.Visible = false;     
                    this.plcNoData.Visible = true;
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }

        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;
            if (row.RowType == DataControlRowType.DataRow)
            {
               
                Label lbl = row.FindControl("lblActType") as Label;

                var dr = row.DataItem as DataRowView;
                int actType = dr.Row.Field<int>("ActType");

                if (actType == 0)
                {
                    lbl.Text = "支出";
                }

                else
                {
                    lbl.Text = "收入";
                }
                if (dr.Row.Field<int>("Amount") > 1000)
                {
                    lbl.ForeColor = Color.Red;
                }

            }
        }
    }
}