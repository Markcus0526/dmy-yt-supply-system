using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiTongBackend.Models;
using System.Web.Security;
using YiTongBackend.Models.Library;

namespace YiTongBackend.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        #region property
            private AccountModel accountModel = new AccountModel();
        #endregion

        public ActionResult Index()
        {
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Account";
            ViewData["level2nav"] = "Account";

            return View();
        }

        public ActionResult LogOn(string returnUrl)
        {
            string addr = Request.UserHostAddress;
            
            ViewData["rootUri"] = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            ViewData["base_url"] = ViewData["rootUri"] + "Account";
            ViewData["returnUrl"] = returnUrl;
            ViewData["Message"] = "";

            var ssw = HttpContext.Session["adminId"];

            if (ssw != null)
            {
                if (User.Identity.IsAuthenticated)
                {

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Banner", "Banner");
                    }
                }
            }

            return View("LogOn");
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            string addr = Request.UserHostAddress;


            ViewData["rootUri"] = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            ViewData["base_url"] = ViewData["rootUri"] + "Account";

            ViewData["curdate"] = DateTime.Today.ToString("yyyy年MM月dd日");
            ViewData["Message"] = "";

            if (!ApkLicenseService.CheckTrialVersion())
            {
                ModelState.AddModelError("modelerror", "系统到期了，已使用了30天！");
                return View("LogOn", model);
            }
            if (ModelState.IsValid)
            {
                var userInfo = accountModel.ValidateUser(model.UserName, model.Password);
                if (userInfo != null)
                {
                    ViewData["Message"] = "";

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                userInfo.userid,
                                DateTime.Now,
                                DateTime.Now.AddMinutes(2880),
                                model.RememberMe,
                                userInfo.uid + "|" + userInfo.userid,
                                FormsAuthentication.FormsCookiePath);

                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Banner", "Banner");
                    }
                }
                else
                {
                    ModelState.AddModelError("modelerror", "帐号或密码错误，请重新输入");
                }

                return View("LogOn", model);
            }
            else
            {
                ModelState.AddModelError("modelerror", "帐号或密码错误，请重新输入");
            }

            return View("LogOn", model);
        }
        
        public ActionResult LogOff()
        {
            accountModel.SignOut();

            return RedirectToAction("LogOn", "Account");
        }
    }
}
