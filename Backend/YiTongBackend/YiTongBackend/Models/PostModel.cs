using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YiTongBackend.Models.Library;
using System.Collections.Specialized;

namespace YiTongBackend.Models
{
    public class POST_SUBMITSTATUS
    {
        public const string DUPLICATE_LOGINID = "操作失败： 用户账号重复！";
        public const string SUCCESS_SUBMIT = "";
        public const string ERROR_SUBMIT = "操作失败";
        public const string CANNOT_DOWNLEVEL = "无法下级角色, 还存在此用户的2级代理";
    }

    public class PostModel
    {
        public IEnumerable<tbl_post> GetPostList()
        {
            YiTongDBDataContext db = new YiTongDBDataContext();
            
            IEnumerable<tbl_post> retList = null;
            retList = db.tbl_posts
                .Where(p => p.deleted == 0)
                .OrderBy(p => p.uid);

            return retList;
        }
        
        public JqDataTableInfo GetPostDataTable(JQueryDataTableParamModel param, NameValueCollection Request, String rootUri)
        {
            JqDataTableInfo rst = new JqDataTableInfo();
            IEnumerable<tbl_post> filteredCompanies;

            var alllist = GetPostList();
            filteredCompanies = alllist;
            
            var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies
                         select new[] { 
                Convert.ToString(c.uid),
                c.postcontent,
                c.viewurl,
                (c.isvisible == 1)?"显示":"不显示",
                String.Format("{0:yyyy-MM-dd HH:mm:ss}", c.regdate),
                Convert.ToString(c.uid)
            };

            rst.sEcho = param.sEcho;
            rst.iTotalRecords = alllist.Count();
            rst.iTotalDisplayRecords = filteredCompanies.Count();
            rst.aaData = result;

            return rst;
        }

        public static tbl_post GetPostInfo(long id)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            return db.tbl_posts
                .Where(p => p.deleted == 0 && p.uid == id)
                .FirstOrDefault();
        }

        public bool DeletePost(long id)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            var delitem = (from m in db.tbl_posts
                           where m.deleted == 0 && m.uid == id
                           select m).FirstOrDefault();

            if (delitem != null)
            {
                delitem.deleted = 1;
                db.SubmitChanges();
            }

            return true;
        }

        public string InsertPost(string postcontent, string viewurl, byte isvisible)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            try
            {
                tbl_post newitem = new tbl_post();

                newitem.postcontent = postcontent;
                newitem.viewurl = viewurl;
                newitem.regdate = DateTime.Now;
                newitem.isvisible = isvisible;
                newitem.deleted = 0;

                db.tbl_posts.InsertOnSubmit(newitem);
                db.SubmitChanges();

                return BANNER_SUBMITSTATUS.SUCCESS_SUBMIT;
            }
            catch (Exception e)
            {
                CommonModel.WriteLogFile("PostModel", "InsertPost()", e.ToString());
                return BANNER_SUBMITSTATUS.ERROR_SUBMIT;
            }
        }

        public string UpdatePost(long uid, string postcontent, string viewurl, byte isvisible)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            try
            {
                tbl_post edititem = (from m in db.tbl_posts
                                       where m.deleted == 0 && m.uid == uid
                                       select m).FirstOrDefault();

                if (edititem != null)
                {
                    edititem.postcontent = postcontent;
                    edititem.viewurl = viewurl;
                    edititem.isvisible = isvisible;
                    edititem.deleted = 0;

                    db.SubmitChanges();

                    return BANNER_SUBMITSTATUS.SUCCESS_SUBMIT;
                }
            }
            catch (Exception e)
            {
                CommonModel.WriteLogFile("PostModel", "UpdatePost()", e.ToString());
                return BANNER_SUBMITSTATUS.ERROR_SUBMIT;
            }

            return BANNER_SUBMITSTATUS.ERROR_SUBMIT;
        }
    }
}