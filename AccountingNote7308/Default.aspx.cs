using Accounting.dbSource;
using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote7308
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var dt1 = DfManager.GetStbkpg();
            var dt2 = DfManager.GetEdbkpg();
            var dt3 = DfManager.GetCount();
            var dt4 = DfManager.GetMembers();

            var dt5 = dt3.Rows[0]["Amount"].ToString();
            var dt6 = dt4.Rows[0]["Name"].ToString();

            this.ltStbkpg.Text = dt1.Rows[0]["CreateDate"].ToString();
            this.ltEdbkpg.Text = dt2.Rows[0]["CreateDate"].ToString();
            this.ltcount.Text = $"共{dt5}筆";
            this.ltMembers.Text = $"共{dt6}人";
        }

       
        protected void btnLogin_Click(object sender, EventArgs e){}
    }
}