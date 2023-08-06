using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Configuration;

namespace YiTongWebService.ServiceModel
{
    public enum ServiceError
    {
        ERR_SUCCESS = 0, //Success
        ERR_PARAM_INVALID = -1, 
        ERR_INTERNAL_EXCEPTION = 500
    }

    [DataContract]
    [Newtonsoft.Json.JsonObject(MemberSerialization = Newtonsoft.Json.MemberSerialization.OptIn)]
    public class ServiceResponseData
    {
        [DataMember(Name = "SVCC_RETVAL", Order = 1)]
        public ServiceError RetVal { get; set; }

        object retdata = new object();
        [DataMember(Name = "SVCC_DATA", Order = 2), Newtonsoft.Json.JsonProperty]
        public object Data
        {
            get { return retdata; }
            set { retdata = value; }
        }

        String baseurl = ConfigurationManager.AppSettings["ServerRootUri"];
        [DataMember(Name = "SVCC_BASEURL", Order = 3)]
        public String BaseUrl
        {
            get { return baseurl; }
            set { baseurl = value; }
        }
    }

    [DataContract]
    public class STAdvert
    {
        [DataMember(Name = "Id", Order = 1)]
        public long uid { get; set; }
        
        [DataMember(Name = "Title", Order = 2)]
        public string title { get; set; }

        [DataMember(Name = "ImgPath", Order = 3)]
        public string imgpath { get; set; }        
    }

    [DataContract]
    public class STBanner
    {
        [DataMember(Name = "Id", Order = 1)]
        public long uid { get; set; }
        
        [DataMember(Name = "Title", Order = 2)]
        public string title { get; set; }

        [DataMember(Name = "LinkURL", Order = 3)]
        public string linkurl { get; set; }        
    }

    [DataContract]
    public class STPost
    {
        [DataMember(Name = "Id", Order = 1)]
        public long uid { get; set; }

        [DataMember(Name = "Content", Order = 2)]
        public string content { get; set; }

        [DataMember(Name = "ViewURL", Order = 3)]
        public string viewurl { get; set; }
    }

    public class YTServiceModel
    {
        public int ADVERT_MAX_ITEMCOUNT = 6;

        public ServiceResponseData RetrieveNewVersion(string version)
        {
            YiTongDBDataContext db = new YiTongDBDataContext();
            ServiceResponseData result = new ServiceResponseData();

            int nNewVersion = 0;
            string serverVersion = string.Empty, serverURL = string.Empty;

            try
            {
                serverVersion = (from m in db.tbl_envs
                                 where m.item.Equals("androidver")
                                 select m.value).First();
                serverURL = (from m in db.tbl_envs
                             where m.item.Equals("androidurl")
                             select m.value).First();

                if (serverVersion != null)
                {
                    nNewVersion = AvailableNewVersion(version, serverVersion);

                    result.RetVal = ServiceError.ERR_SUCCESS;
                }
                else
                {
                    result.RetVal = ServiceError.ERR_PARAM_INVALID;
                }
            }
            catch (System.Exception ex)
            {
                result.RetVal = ServiceError.ERR_PARAM_INVALID;
            }

            if (nNewVersion == 1)
                result.Data = serverURL;
            else
                result.Data = string.Empty;

            return result;
        }

        public int AvailableNewVersion(string oldVersion, string newVersion)
        {
            int pos = oldVersion.LastIndexOf(".");

            int oldMajor = 0, oldMinor = 0;
            if (pos > 0)
            {
                oldMajor = Convert.ToInt32(oldVersion.Substring(0, pos));
                oldMinor = Convert.ToInt32(oldVersion.Substring(pos + 1, oldVersion.Length - pos - 1));
            }
            else
                return 0;

            pos = newVersion.LastIndexOf(".");
            int newMajor = 0, newMinor = 0;
            if (pos > 0)
            {
                newMajor = Convert.ToInt32(newVersion.Substring(0, pos));
                newMinor = Convert.ToInt32(newVersion.Substring(pos + 1, newVersion.Length - pos - 1));
            }
            else
                return 0;

            if (oldMajor < newMajor)
                return 1;
            else if (oldMajor == newMajor)
            {
                if (oldMinor < newMinor)
                    return 1;
                else
                    return 0;
            }
            else
                return 0;
        }

        public ServiceResponseData RetrieveAdvertList()
        {
            YiTongDBDataContext db = new YiTongDBDataContext();
            ServiceResponseData result = new ServiceResponseData();

            try
            {
                List<tbl_advert> listData = (from m in db.tbl_adverts
                                            where m.deleted == 0
                                            orderby m.regtime descending
                                            select m).ToList();

                List<STAdvert> listAdvert = new List<STAdvert>();
                for (int i = 0; i < listData.Count; i++)
                {
                    STAdvert advert = new STAdvert();
                    advert.uid = listData[i].uid;
                    advert.title = listData[i].title;
                    advert.imgpath = listData[i].imgpath;

                    listAdvert.Add(advert);
                }

                result.RetVal = ServiceError.ERR_SUCCESS;
                result.Data = listAdvert.Take((listAdvert.Count > ADVERT_MAX_ITEMCOUNT) ? ADVERT_MAX_ITEMCOUNT : listAdvert.Count).ToArray();
            }
            catch
            {
                result.RetVal = ServiceError.ERR_INTERNAL_EXCEPTION;
            }

            return result;
        }

        public ServiceResponseData RetrieveSiteAddr()
        {
            YiTongDBDataContext db = new YiTongDBDataContext();
            ServiceResponseData result = new ServiceResponseData();

            string serverURL = string.Empty;

            try
            {
                serverURL = (from m in db.tbl_envs
                             where m.item.Equals("weburl")
                             select m.value).First();

                if (serverURL != null)
                {
                    result.RetVal = ServiceError.ERR_SUCCESS;
                }
                else
                {
                    serverURL = string.Empty;
                    result.RetVal = ServiceError.ERR_INTERNAL_EXCEPTION;
                }
            }
            catch
            {
                result.RetVal = ServiceError.ERR_INTERNAL_EXCEPTION;
            }

            result.Data = serverURL;

            return result;
        }

        public ServiceResponseData RetrieveBannerList()
        {
            YiTongDBDataContext db = new YiTongDBDataContext();
            ServiceResponseData result = new ServiceResponseData();

            try
            {
                List<tbl_banner> listData = (from m in db.tbl_banners
                                            where m.deleted == 0
                                            orderby m.regtime descending
                                            select m).ToList();

                List<STBanner> listBanner = new List<STBanner>();
                for (int i = 0; i < listData.Count; i++)
                {
                    STBanner banner = new STBanner();
                    banner.uid = listData[i].uid;
                    banner.title = listData[i].title;
                    banner.linkurl = listData[i].linkurl;

                    listBanner.Add(banner);
                }

                result.RetVal = ServiceError.ERR_SUCCESS;
                result.Data = listBanner;
            }
            catch
            {
                result.RetVal = ServiceError.ERR_INTERNAL_EXCEPTION;
            }

            return result;
        }

        public ServiceResponseData GetSplashImgPath()
        {
            YiTongDBDataContext db = new YiTongDBDataContext();
            ServiceResponseData result = new ServiceResponseData();

            string imgPath = string.Empty;

            try
            {
                imgPath = (from m in db.tbl_envs
                             where m.item.Equals("imgpath")
                             select m.value).First();

                if (imgPath != null)
                {
                    result.RetVal = ServiceError.ERR_SUCCESS;
                }
                else
                {
                    imgPath = string.Empty;
                    result.RetVal = ServiceError.ERR_INTERNAL_EXCEPTION;
                }
            }
            catch
            {
                result.RetVal = ServiceError.ERR_INTERNAL_EXCEPTION;
            }

            result.Data = imgPath;

            return result;
        }

        public ServiceResponseData RetrievePostList()
        {
            YiTongDBDataContext db = new YiTongDBDataContext();
            ServiceResponseData result = new ServiceResponseData();

            try
            {
                List<tbl_post> listData = (from m in db.tbl_posts
                                             where m.deleted == 0 && m.isvisible == 1
                                             orderby m.regdate descending
                                             select m).ToList();

                List<STPost> listPost = new List<STPost>();
                for (int i = 0; i < listData.Count; i++)
                {
                    STPost postItem = new STPost();
                    postItem.uid = listData[i].uid;
                    postItem.content = listData[i].postcontent;
                    postItem.viewurl = listData[i].viewurl;

                    listPost.Add(postItem);
                }

                result.RetVal = ServiceError.ERR_SUCCESS;
                result.Data = listPost;
            }
            catch
            {
                result.RetVal = ServiceError.ERR_INTERNAL_EXCEPTION;
            }

            return result;
        }
    }
}