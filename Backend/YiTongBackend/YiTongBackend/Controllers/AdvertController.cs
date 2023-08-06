using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiTongBackend.Models;
using YiTongBackend.Models.Library;

namespace YiTongBackend.Controllers
{
    public class AdvertController : Controller
    {
        //
        // GET: /Advert/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Advert()
        {
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Advert";
            ViewData["level2nav"] = "Advert";

            return View();
        }

        [AjaxOnly]
        public JsonResult RetrieveAdvertList(JQueryDataTableParamModel param)
        {
            AdvertModel aModel = new AdvertModel();
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            JqDataTableInfo rst = aModel.GetAdvertDataTable(param, Request.QueryString, rootUri);
            return Json(rst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddAdvert()
        {
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Advert";
            ViewData["level2nav"] = "Advert";

            return View();
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult SubmitAdvert(string uid, string imgpath, string title)
        {
            AdvertModel aModel = new AdvertModel();
            string rst = "";

            //byte isenabled = (byte)(enabled == "on" ? 1 : 0);
            long uuId = 0;
            try { uuId = long.Parse(uid); }
            catch (System.Exception ex) { }

            if (uuId == 0)
            {

                rst = aModel.InsertAdvert(imgpath, title);
            }
            else
            {
                rst = aModel.UpdateAdvert(uuId, imgpath, title);
            }

            return Json(rst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditAdvert(int id)
        {
            AdvertModel amodel = new AdvertModel();
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Advert";
            ViewData["level2nav"] = "Advert";

            tbl_advert info = AdvertModel.GetAdvertInfo(id);
            ViewData["uid"] = info.uid;
            ViewData["title"] = info.title;
            ViewData["imgpath"] = info.imgpath;
            ViewData["regtime"] = info.regtime;

            return View("AddAdvert");
        }

        [HttpPost]
        public JsonResult DeleteAdvert(long id)
        {
            AdvertModel aModel = new AdvertModel();
            bool rst = aModel.DeleteAdvert(id);

            return Json(rst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSelectedAdvert(string ids)
        {
            string[] idlist = ids.Split(new char[] { ',' });

            AdvertModel aModel = new AdvertModel();

            for (int i = 0; i < idlist.Length; i++)
            {
                if (idlist[i] == "on") continue;
                long id = Convert.ToInt64(idlist[i]);
                aModel.DeleteAdvert(id);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
