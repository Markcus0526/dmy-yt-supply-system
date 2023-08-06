using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YiTongBackend.Models.Library;
using System.Collections.Specialized;

namespace YiTongBackend.Models
{
    public class ADVERT_SUBMITSTATUS
    {
        public const string DUPLICATE_LOGINID = "操作失败： 用户账号重复！";
        public const string SUCCESS_SUBMIT = "";
        public const string ERROR_SUBMIT = "操作失败";
        public const string CANNOT_DOWNLEVEL = "无法下级角色, 还存在此用户的2级代理";
    }

    public class AdvertModel
    {
        public IEnumerable<tbl_advert> GetAdvertList()
        {
            YiTongDBDataContext _context = new YiTongDBDataContext();
            
            IEnumerable<tbl_advert> retList = null;
            retList = _context.tbl_adverts
                .Where(p => p.deleted == 0)
                .OrderBy(p => p.uid);

            return retList;
        }

        public JqDataTableInfo GetAdvertDataTable(JQueryDataTableParamModel param, NameValueCollection Request, String rootUri)
        {
            JqDataTableInfo rst = new JqDataTableInfo();
            IEnumerable<tbl_advert> filteredCompanies;

            var alllist = GetAdvertList();
            filteredCompanies = alllist;
            
            var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies
                         select new[] { 
                            Convert.ToString(c.uid),
                            (c.title.Length > 10)?c.title.Substring(0, 10) + "...":c.title,
                            c.imgpath,
                            String.Format("{0:yyyy-MM-dd HH:mm:ss}", c.regtime),
                            Convert.ToString(c.uid)
            };

            rst.sEcho = param.sEcho;
            rst.iTotalRecords = alllist.Count();
            rst.iTotalDisplayRecords = filteredCompanies.Count();
            rst.aaData = result;

            return rst;
        }

        public static tbl_advert GetAdvertInfo(long id)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            return db.tbl_adverts
                .Where(p => p.deleted == 0 && p.uid == id)
                .FirstOrDefault();
        }

        public string InsertAdvert(string imgpath, string title)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            try
            {
                tbl_advert newitem = new tbl_advert();

                if (imgpath == null || imgpath.Length == 0)
                    imgpath = "";

                newitem.imgpath = imgpath;                
                newitem.title = title;
                newitem.regtime = DateTime.Now;
                newitem.deleted = 0;

                db.tbl_adverts.InsertOnSubmit(newitem);
                db.SubmitChanges();

                return ADVERT_SUBMITSTATUS.SUCCESS_SUBMIT;
            }
            catch (Exception e)
            {
                CommonModel.WriteLogFile("AdvertModel", "InsertAdvert()", e.ToString());
                return ADVERT_SUBMITSTATUS.ERROR_SUBMIT;
            }
        }

        public string UpdateAdvert(long uid, string imgpath, string title)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            try
            {
                tbl_advert edititem = (from m in db.tbl_adverts
                                       where m.deleted == 0 && m.uid == uid
                                       select m).FirstOrDefault();

                if (edititem != null)
                {
                    if (imgpath == null || imgpath.Length == 0)
                        imgpath = "";

                    edititem.imgpath = imgpath;
                    edititem.title = title;
                    edititem.deleted = 0;

                    db.SubmitChanges();

                    return ADVERT_SUBMITSTATUS.SUCCESS_SUBMIT;
                }
            }
            catch (Exception e)
            {
                CommonModel.WriteLogFile("AdvertModel", "UpdateAdvert()", e.ToString());
                return ADVERT_SUBMITSTATUS.ERROR_SUBMIT;
            }

            return ADVERT_SUBMITSTATUS.ERROR_SUBMIT;
        }

        public bool DeleteAdvert(long id)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            var delitem = (from m in db.tbl_adverts
                           where m.deleted == 0 && m.uid == id
                           select m).FirstOrDefault();

            if (delitem != null)
            {
                delitem.deleted = 1;
                db.SubmitChanges();
            }

            return true;
        }
    }
}