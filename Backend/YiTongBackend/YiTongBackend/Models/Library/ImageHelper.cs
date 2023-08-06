using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Web.Hosting;
using YiTongBackend.Models;

namespace YiTongBackend.Models.Library
{
    #region ImageCropModel
    public class GiftImageSize {
        public int p_width {get;set;}
        public int p_height {get;set;}
    }

    public static class CropImageSizes
    {
    };

    public static class UpImageCategory //Set the Content/uploads/ directory name
    {
    };
    #endregion

    public class ImageHelper
    {
        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = this.getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        public string ResizeAndCrop(string filepath, decimal x, decimal y, decimal w, decimal h, string category, string imagesize)
        {
            string rootpath = HostingEnvironment.MapPath("~/");
            string file_prefix = "";
            int width = 0, height = 0;

            int orgWidth = 0, orgHeight = 0;
            int tgtX, tgtY, tgtW, tgtH;

            string[] sizelist = imagesize.Split(',');
            height = (int)decimal.Parse(sizelist[1]);
            width = (int)decimal.Parse(sizelist[0]);

            file_prefix = category + "_";

            //first get original image bitmap.
            Image orgimg = Image.FromFile(rootpath + filepath);

            if (orgimg == null) return "";

            orgWidth = orgimg.Width;
            orgHeight = orgimg.Height;


            /* Get ratio to fit target image to original image size. */
            /*
            if ((orgHeight / (decimal)orgWidth) <= (height / (decimal)width))
            {
                zoomratio = orgHeight / (decimal)height;
            }
            else
            {
                zoomratio = orgWidth / (decimal)width;
            }
             * */

            decimal zoomratio = 1;
            /*
             * 2014-03-24
             * /
            
            if (orgimg.Width > width)
                zoomratio = 1;
            else
                zoomratio = (decimal)orgimg.Width / width;

             decimal zoomratioY = 1;
             if (orgimg.Height > height)
                 zoomratioY = 1;
             else
                 zoomratioY = (decimal)orgimg.Height / height;
            */
            //zoomratio = (zoomratio > zoomratioY)? zoomratioY: zoomratio;

            if (orgimg.Height > 300)
            {
                zoomratio = (decimal)orgimg.Height / 300;
            }

            /* Fit target image size to orginal image size using computed ratio. */
            tgtX = (int)(x * zoomratio);
            tgtY = (int)(y * zoomratio);
            tgtW = (int)((w * zoomratio) - 1);
            tgtH = (int)((h * zoomratio) - 1);

            Bitmap srcBmp = orgimg as Bitmap;

            Rectangle cropSection = new Rectangle(tgtX, tgtY, tgtW, tgtH);

            //string rstname = "Content/uploads/image/" + category + "/" + file_prefix + Path.GetFileName(filepath);
            string rndFileName = Path.GetFileName(filepath);// Remove the original filename.
            int nPos = rndFileName.LastIndexOf('_');
            if (nPos > 0)
                rndFileName = rndFileName.Substring(nPos+1, rndFileName.Length - nPos - 1);
            string rstname = "Content/uploads/image/" + category + "/" + file_prefix + rndFileName;
            try
            {
                /* Make fited and cropped image from original image. */
                using (Bitmap cropBmp = srcBmp.Clone(cropSection, srcBmp.PixelFormat))
                {
                    using (Bitmap targetBmp = new Bitmap(width, height))
                    {
                        using (Graphics g = Graphics.FromImage((Image)targetBmp))
                        {
                            g.Clear(Color.Transparent);
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.DrawImage(cropBmp, 0, 0, width, height);

                            string savepath = rootpath + rstname;
                            try
                            {
                                targetBmp.Save(savepath, orgimg.RawFormat);
                            }
                            catch (Exception e)
                            {
                                CommonModel.WriteLogFile(this.GetType().Name, "ResizeAndCrop()", e.ToString());
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonModel.WriteLogFile("ImageHelper", "ResizeAndCrop()", ex.ToString());            	
            }

            orgimg.Dispose();

            return rstname;
        }
    }
}