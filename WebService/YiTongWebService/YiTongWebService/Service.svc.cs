using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YiTongWebService.ServiceModel;

namespace YiTongWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    public class Service : IService
    {
        YTServiceModel aModel = new YTServiceModel();

        public ServiceResponseData GetNewVersion(string version)
        {
            return aModel.RetrieveNewVersion(version);
        }

        public ServiceResponseData GetAdvertList()
        {
            return aModel.RetrieveAdvertList();
        }

        public ServiceResponseData GetSiteAddr()
        {
            return aModel.RetrieveSiteAddr();
        }

        public ServiceResponseData GetBannerList()
        {
            return aModel.RetrieveBannerList();
        }

        public ServiceResponseData GetSplashImgPath()
        {
            return aModel.GetSplashImgPath();
        }

        public ServiceResponseData GetPostList()
        {
            return aModel.RetrievePostList();
        }
    }
}
