using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace YiTongBackend.Models
{
    public class LogOnModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        [DisplayName("用户名:")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("密码:")]
        public string Password { get; set; }

        [DisplayName("下次自动登录")]
        public bool RememberMe { get; set; }
    }

    public class Foreign_Servant
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "{0}至少为{1}位.";
        private readonly int _minCharacters = 1;

        public ValidatePasswordLengthAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name, _minCharacters);
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;

            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }
    }

    public class AccountModel
    {
        public tbl_admin ValidateUser(string username, string password)
        {
            tbl_admin userObj = GetUserInfoByUserID(username, password);

            if (userObj != null)
                return userObj;

            return null;
        }

        public tbl_admin GetUserInfoByUserID(string userId, string password)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            try
            {
                string md5pass = CommonModel.GetMD5Hash(password);
                tbl_admin userinfo = (from m in db.tbl_admins
                                      where ((m.userid.ToLower() == userId.ToLower() &&
                                        m.password == md5pass && m.deleted == 0))
                                      select m).FirstOrDefault();

                if (userinfo != null)
                {
                    return userinfo;
                }
            }
            catch (Exception e)
            {
                CommonModel.WriteLogFile(this.GetType().Name, "GetUserInfoByUserID()", e.ToString());
            }
            return null;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}