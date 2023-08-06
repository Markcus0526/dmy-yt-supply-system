using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using YiTongBackend.Models;
using YiTongBackend.Models.Library;
using System.Globalization;

namespace YiTongBackend.Controllers
{
    public class BannerController : Controller
    {
        //
        // GET: /Banner/

        public ActionResult Banner()
        {
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Banner";
            ViewData["level2nav"] = "Banner";

            return View();
        }

        [AjaxOnly]
        public JsonResult RetrieveBannerList(JQueryDataTableParamModel param)
        {
            BannerModel aModel = new BannerModel();
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            JqDataTableInfo rst = aModel.GetBannerDataTable(param, Request.QueryString, rootUri);
            return Json(rst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult SubmitBanner(string uid, string title, string linkurl)
        {
            BannerModel aModel = new BannerModel();
            string rst = "";

            //byte isenabled = (byte)(enabled == "on" ? 1 : 0);
            long uuId = 0;
            try { uuId = long.Parse(uid); }
            catch (System.Exception ex) { }

            if (uuId == 0)
            {

                rst = aModel.InsertBanner(title, linkurl);
            }
            else
            {
                rst = aModel.UpdateBanner(uuId, title, linkurl);
            }

            return Json(rst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddBanner()
        {
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Banner";
            ViewData["level2nav"] = "Banner";

            return View();
        }

        public ActionResult EditBanner(int id)
        {
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Banner";
            ViewData["level2nav"] = "Banner";

            tbl_banner info = BannerModel.GetBannerInfo(id);
            ViewData["uid"] = info.uid;
            ViewData["title"] = info.title;
            ViewData["linkurl"] = info.linkurl;

            return View("AddBanner");
        }

        [HttpPost]
        public JsonResult DeleteBanner(long id)
        {
            BannerModel aModel = new BannerModel();
            bool rst = aModel.DeleteBanner(id);

            return Json(rst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSelectedBanner(string ids)
        {
            string[] idlist = ids.Split(new char[] { ',' });
            //long[] idarray = new long[idlist.Length];

            BannerModel aModel = new BannerModel();

            for (int i = 0; i < idlist.Length; i++)
            {
                if (idlist[i] == "on") continue;
                long id = Convert.ToInt64(idlist[i]);
                aModel.DeleteBanner(id);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
