using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YiTongBackend.Models.Library;
using System.Collections.Specialized;

namespace YiTongBackend.Models
{
    public class BANNER_SUBMITSTATUS
    {
        public const string DUPLICATE_LOGINID = "操作失败： 用户账号重复！";
        public const string SUCCESS_SUBMIT = "";
        public const string ERROR_SUBMIT = "操作失败";
        public const string CANNOT_DOWNLEVEL = "无法下级角色, 还存在此用户的2级代理";
    }

    public class BannerModel
    {
        public IEnumerable<tbl_banner> GetBannerList()
        {
            YiTongDBDataContext db = new YiTongDBDataContext();
            
            IEnumerable<tbl_banner> retList = null;
            retList = db.tbl_banners
                .Where(p => p.deleted == 0)
                .OrderBy(p => p.uid);

            return retList;
        }

        public JqDataTableInfo GetBannerDataTable(JQueryDataTableParamModel param, NameValueCollection Request, String rootUri)
        {
            JqDataTableInfo rst = new JqDataTableInfo();
            IEnumerable<tbl_banner> filteredCompanies;

            var alllist = GetBannerList();
            filteredCompanies = alllist;
            
            var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies
                         select new[] { 
                Convert.ToString(c.uid),
                c.title,
                c.linkurl,
                String.Format("{0:yyyy-MM-dd HH:mm:ss}", c.regtime),
                Convert.ToString(c.uid)
            };

            rst.sEcho = param.sEcho;
            rst.iTotalRecords = alllist.Count();
            rst.iTotalDisplayRecords = filteredCompanies.Count();
            rst.aaData = result;

            return rst;
        }

        public static tbl_banner GetBannerInfo(long id)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            return db.tbl_banners
                .Where(p => p.deleted == 0 && p.uid == id)
                .FirstOrDefault();
        }

        public bool DeleteBanner(long id)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            var delitem = (from m in db.tbl_banners
                           where m.deleted == 0 && m.uid == id
                           select m).FirstOrDefault();

            if (delitem != null)
            {
                delitem.deleted = 1;
                db.SubmitChanges();
            }

            return true;
        }

        public string InsertBanner(string title, string linkurl)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            try
            {
                tbl_banner newitem = new tbl_banner();

                newitem.title = title;
                newitem.regtime = DateTime.Now;
                newitem.linkurl = linkurl;
                newitem.deleted = 0;

                db.tbl_banners.InsertOnSubmit(newitem);
                db.SubmitChanges();

                return BANNER_SUBMITSTATUS.SUCCESS_SUBMIT;
            }
            catch (Exception e)
            {
                CommonModel.WriteLogFile("BannerModel", "InsertBanner()", e.ToString());
                return BANNER_SUBMITSTATUS.ERROR_SUBMIT;
            }
        }

        public string UpdateBanner(long uid, string title, string linkurl)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            try
            {
                tbl_banner edititem = (from m in db.tbl_banners
                                       where m.deleted == 0 && m.uid == uid
                                       select m).FirstOrDefault();

                if (edititem != null)
                {
                    edititem.title = title;
                    edititem.linkurl = linkurl;
                    edititem.deleted = 0;

                    db.SubmitChanges();

                    return BANNER_SUBMITSTATUS.SUCCESS_SUBMIT;
                }
            }
            catch (Exception e)
            {
                CommonModel.WriteLogFile("BannerModel", "UpdateBanner()", e.ToString());
                return BANNER_SUBMITSTATUS.ERROR_SUBMIT;
            }

            return BANNER_SUBMITSTATUS.ERROR_SUBMIT;
        }
    }
}