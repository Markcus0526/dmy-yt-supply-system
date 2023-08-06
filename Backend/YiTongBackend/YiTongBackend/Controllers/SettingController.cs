using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiTongBackend.Models;

namespace YiTongBackend.Controllers
{
    public class SettingController : Controller
    {
        //
        // GET: /Setting/        
        public ActionResult Setting()
        {
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Setting";
            ViewData["level2nav"] = "Setting";

            SettingsData sysSetting = SettingModel.GetSettingsData();
            ViewData["androidver"] = sysSetting.androidver;
            ViewData["androidurl"] = sysSetting.androidurl;
            ViewData["weburl"] = sysSetting.weburl;
            ViewData["imgpath"] = sysSetting.imgpath;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitSetting(FormCollection actionValues)
        {
            string rst = "";

            string androidver = actionValues["androidver"];
            string androidurl = actionValues["androidurl"];
            string weburl = actionValues["weburl"];
            string imgpath = actionValues["imgpath"];

            rst = SettingModel.UpdateParam("androidver", androidver);
            rst = SettingModel.UpdateParam("androidurl", androidurl);
            rst = SettingModel.UpdateParam("weburl", weburl);
            rst = SettingModel.UpdateParam("imgpath", imgpath);

            return Json(rst, JsonRequestBehavior.AllowGet);
        }

    }
}
