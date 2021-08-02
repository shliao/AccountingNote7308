﻿using Accounting.dbSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    public class AuthManager
    {
        public static bool Islogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }
        public static UserInfoModel GetCurrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            if (account == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }

            DataRow dr = UserInfoManager.GetUserInfoListbyAccount(account);

            if (dr == null)
                return null;

            UserInfoModel model = new UserInfoModel();
            model.ID = dr["ID"].ToString();
            model.Account = dr["Account"].ToString();
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();

            return model;
        }
        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }
        public static bool tryLogin(string account, string pwd, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "Account / Password is needed.";
                return false;
            }

            var dr = UserInfoManager.GetUserInfoListbyAccount(account);

            if (dr == null)
            {
                errorMsg = "Account is not exist.";
                return false;
            }

            if (string.Compare(dr["Account"].ToString(), account) == 0 &&
                string.Compare(dr["PWD"].ToString(), pwd) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = dr["Account"].ToString();
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "Login Fail.Please check Account / Password.";
                return false;
            }
        }
    }
}
