using Accounting.dbSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote7308.SystemAdmin
{
    public partial class AccountingDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["UserLoginInfo"] == null)       
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            string account = this.Session["UserLoginInfo"] as string;
            var drUserInfo = UserInfoManager.GetUserInfobyAccount(account);

            if (drUserInfo == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            if (!this.IsPostBack)
            {
               
                if (this.Request.QueryString["ID"] == null)  
                {
                    this.btnDelete.Visible = false;
                }
                else
                {
                    this.btnDelete.Visible = true;
                    string idText = this.Request.QueryString["ID"];
                    int id;
                    if (int.TryParse(idText, out id))
                    {
                        var drAccounting = AccountingManager.GetAccounting(id, drUserInfo["ID"].ToString());  


                        if (drAccounting == null)
                        {
                            this.ltMsg.Text = "記帳不存在";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else
                        {                                                             
                            this.ddIActType.SelectedValue = drAccounting["ActType"].ToString();
                            this.txtAmount.Text = drAccounting["Amount"].ToString();
                            this.txtCaption.Text = drAccounting["Caption"].ToString();
                            this.txtDesc.Text = drAccounting["Body"].ToString();
                        }
                    }
                    else
                    {
                        this.ltMsg.Text = "需要使用者的ID";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                    }

                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string account = this.Session["UserLoginInfo"] as string;
            var dr = UserInfoManager.GetUserInfobyAccount(account);

            if (dr == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            string userID = dr["ID"].ToString();
            string actTypeText = this.ddIActType.SelectedValue;   
            string amountText = this.txtAmount.Text;
            string caption = this.txtCaption.Text;
            string body = this.txtDesc.Text;

            int amount = Convert.ToInt32(amountText);
            int actType = Convert.ToInt32(actTypeText);

            string idText = this.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(idText))
            {
                AccountingManager.CreateAccounting(userID, caption, amount, actType, body);
            }
            else
            {
                int id;
                if (int.TryParse(idText, out id))
                {
                    AccountingManager.UpdaateAccounting(id, userID, caption, amount, actType, body);
                }
            }

            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList)  
        {
            List<string> msglist = new List<string>(); 

            
            if (this.ddIActType.SelectedValue != "0" &&
                this.ddIActType.SelectedValue != "1")
            {
                msglist.Add("類別必須是1或0");
            }
            
            if (string.IsNullOrWhiteSpace(this.txtAmount.Text))
                msglist.Add("必須是金額");

            else
            {
                int tempInt;
                if (!int.TryParse(this.txtAmount.Text, out tempInt))  
                {
                    msglist.Add("金額必須是數字");
                }

                if (tempInt < 0 || tempInt > 1000000)
                {
                    msglist.Add("金額應在0至1,000,000之間");
                }
            }

            errorMsgList = msglist;

            if (msglist.Count == 0)
                return true;
            else
                return false;        
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idText = this.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(idText))
                return;
            int id;
            if (int.TryParse(idText, out id))
            {
                AccountingManager.DeleteAccounting(id);
            }

            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }
    }
}