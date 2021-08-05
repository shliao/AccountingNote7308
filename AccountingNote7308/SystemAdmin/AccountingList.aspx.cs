using Accounting.dbSource;
using AccountingNote.Auth;
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
            if (!AuthManager.Islogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var dt = AccountingManager.GetAccountingList(currentUser.ID);

            if (dt.Rows.Count > 0)
            {
                var dtPaged = this.GetPagedDataTable(dt);

                this.gvAccountingList.DataSource = dtPaged;
                this.gvAccountingList.DataBind();

                int inc = 0;
                int exp = 0;

                var dt1 = AccountingManager.GetIncome(currentUser.ID);
                var dt2 = AccountingManager.GetExpenses(currentUser.ID);
                var income = dt1.Rows[0]["AT"];
                var expense = dt2.Rows[0]["ATS"];

                if (income != System.DBNull.Value && expense != System.DBNull.Value)
                {
                    inc = (int)income;
                    exp = (int)expense;
                    
                }
                else if (income == System.DBNull.Value)
                {
                    inc = 0;
                    exp = (int)expense;
                }
                else if (expense == System.DBNull.Value)
                {
                    inc = (int)income;
                    exp = 0;
                }

                int total = inc - exp;
                this.ltMsg.Text = $"小計{total}元";
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.plcNoData.Visible = true;
                this.ltMsg.Visible = false;
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
                    lbl.ForeColor = Color.Red;
                }
                else
                    lbl.Text = "收入";
            }
        }
        private int GetcurrentPage()
        {
            string txtpage = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(txtpage))
                return 1;

            int intPage;
            if (!int.TryParse(txtpage, out intPage))
                return 1;

            if (intPage <= 0)
                return 1;

            return intPage;
        }
        private DataTable GetPagedDataTable(DataTable dt)
        {
            DataTable dtPaged = dt.Clone();

            int startindex = (this.GetcurrentPage() - 1) * 10;
            int endindex = (this.GetcurrentPage()) * 10;

            if (endindex > dt.Rows.Count)
                endindex = dt.Rows.Count;

            for (var i = startindex; i < endindex; i++)
            {
                DataRow dr = dt.Rows[i];
                var drNew = dtPaged.NewRow();

                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }

                dtPaged.Rows.Add(drNew);
            }
            return dtPaged;
        }
    }
}