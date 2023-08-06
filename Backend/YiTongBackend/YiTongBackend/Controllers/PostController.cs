using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiTongBackend.Models.Library;
using YiTongBackend.Models;

namespace YiTongBackend.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/
        
        public ActionResult Post()
        {
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Post";
            ViewData["level2nav"] = "Post";

            return View();
        }

        public ActionResult AddPost()
        {
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Post";
            ViewData["level2nav"] = "Post";
            ViewData["isvisible"] = 1;

            return View();
        }

        [AjaxOnly]
        public JsonResult RetrievePostList(JQueryDataTableParamModel param)
        {
            PostModel aModel = new PostModel();
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            JqDataTableInfo rst = aModel.GetPostDataTable(param, Request.QueryString, rootUri);
            return Json(rst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult SubmitPost(string uid, string postcontent, string viewurl, string isvisible)
        {
            PostModel aModel = new PostModel();
            string rst = "";

            byte isenabled = (byte)(isvisible == "on" ? 1 : 0);
            long uuId = 0;
            try { uuId = long.Parse(uid); }
            catch (System.Exception ex) {
                uuId = 0;
            }

            if (uuId == 0)
            {

                rst = aModel.InsertPost(postcontent,viewurl, isenabled);
            }
            else
            {
                rst = aModel.UpdatePost(uuId, postcontent, viewurl, isenabled);
            }

            return Json(rst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditPost(int id)
        {
            string rootUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            ViewData["rootUri"] = rootUri;
            ViewData["level1nav"] = "Post";
            ViewData["level2nav"] = "Post";

            tbl_post info = PostModel.GetPostInfo(id);
            ViewData["uid"] = info.uid;
            ViewData["postcontent"] = info.postcontent;
            ViewData["viewurl"] = info.viewurl;
            ViewData["isvisible"] = info.isvisible;

            return View("AddPost");
        }

        [HttpPost]
        public JsonResult DeletePost(long id)
        {
            PostModel aModel = new PostModel();
            bool rst = aModel.DeletePost(id);

            return Json(rst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSelectedPost(string ids)
        {
            string[] idlist = ids.Split(new char[] { ',' });
            //long[] idarray = new long[idlist.Length];

            PostModel aModel = new PostModel();

            for (int i = 0; i < idlist.Length; i++)
            {
                if (idlist[i] == "on") continue;
                long id = Convert.ToInt64(idlist[i]);
                aModel.DeletePost(id);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
