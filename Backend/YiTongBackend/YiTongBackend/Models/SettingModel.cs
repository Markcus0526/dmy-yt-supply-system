using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YiTongBackend.Models
{
    public class SETTINGS_SUBMITSTATUS
    {
        public const string SUCCESS_SUBMIT = "";
        public const string ERROR_SUBMIT = "操作失败";
    }

    public class SettingsData
    {
        public string androidver { get; set; }
        public string androidurl { get; set; }
        public string weburl { get; set; }
        public string imgpath { get; set; }
    }

    public class SettingModel
    {
        public static SettingsData GetSettingsData()
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            SettingsData data = new SettingsData();

            tbl_env selItem = (from m in db.tbl_envs
                               where (m.item == "androidver")
                               select m).SingleOrDefault();
            if (selItem != null)
            {
                data.androidver = selItem.value.ToString();
            }
            else
            {
                data.androidver = "";
            }

            selItem = (from m in db.tbl_envs
                       where (m.item == "androidurl")
                       select m).SingleOrDefault();
            if (selItem != null)
            {
                data.androidurl = selItem.value.ToString();
            }
            else
            {
                data.androidver = "";
            }

            selItem = (from m in db.tbl_envs
                       where (m.item == "weburl")
                       select m).SingleOrDefault();
            if (selItem != null)
            {
                data.weburl = selItem.value.ToString();
            }
            else
            {
                data.weburl = "";
            }
            selItem = (from m in db.tbl_envs
                       where (m.item == "imgpath")
                       select m).SingleOrDefault();
            if (selItem != null)
            {
                data.imgpath = selItem.value.ToString();
            }
            else
            {
                data.imgpath = "";
            }

            return data;
        }

        public static string UpdateParam(string strParam, string strValue)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();

            string rst = SETTINGS_SUBMITSTATUS.SUCCESS_SUBMIT;

            int nRows = 0;
            long nCount = db.tbl_envs
                .Where(m => m.item == strParam)
                .Count();
            if (nCount > 0)
            {
                string updateSql = string.Format("UPDATE tbl_env SET value='{0}' WHERE item='{1}'", strValue, strParam);
                nRows = db.ExecuteCommand(updateSql);
            }
            else
            {
                string insertSql = string.Format("INSERT INTO tbl_env(item, value) VALUES('{0}', '{1}')", strParam, strValue);
                nRows = db.ExecuteCommand(insertSql);
            }
            if (nRows == 0)
            {
                rst = SETTINGS_SUBMITSTATUS.ERROR_SUBMIT;
            }

            return rst;
        }
    }
}