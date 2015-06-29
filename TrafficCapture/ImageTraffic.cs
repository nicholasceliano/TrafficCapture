using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;

namespace TrafficCapture
{
    class ImageTraffic
    {
        private static string tmpFileLocation = Path.GetTempPath() + "ImageRespository\\";
        WebClient wc;

        public enum ImgFormat
        {
            gif,
            png
        }

        internal void DownloadImagesFromURL(string url, ImgFormat frmt)
        {
            List<string> lst = GetAllImageURLListFromWebPage(url);

            int imgCt = 0;
            foreach (string s in lst)
                imgCt = SaveFileInTempFolder(s, imgCt, frmt);
        }

        internal void DeleteDownloadedImages()
        {
            Directory.Delete(tmpFileLocation);
        }

        internal void GetGIFDuration()
        {

            Stream imgSrc = new FileStream(@"C:\Users\00619461\AppData\Local\Temp\ImageRespository\7.gif", FileMode.Open);
            var gg = new GifBitmapDecoder(imgSrc, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

        
        }

        private List<string> GetAllImageURLListFromWebPage(string webpageURL)
        {
            List<string> lstImgURLS = new List<string>();
            HttpWebResponse response = (HttpWebResponse)WebRequest.Create(webpageURL).GetResponse();

            string respText = new StreamReader(response.GetResponseStream()).ReadToEnd();

            foreach (string tag in respText.Split('<'))
            {
                if (IsImageTag(tag))
                {
                    string srcVal = GetSubstringBetweenTags(tag, "src=\"", "\"");
                    lstImgURLS.Add(IsValidURL(srcVal) ? srcVal : webpageURL + srcVal);
                }
            }

            return lstImgURLS;
        }

        private int SaveFileInTempFolder(string url, int imgCt, ImgFormat frmt)
        {
            if (!Directory.Exists(tmpFileLocation))
                Directory.CreateDirectory(tmpFileLocation);

            return DownloadFileTypesFromURL(url, imgCt, frmt);
        }

        #region Helper Methods
        
        private int DownloadFileTypesFromURL(string url, int imgCt, ImgFormat frmt)
        {
            string[] spltURL = url.Split('.');
            if (spltURL[spltURL.Length - 1] == frmt.ToString())
            {
                if (wc == null)
                    wc = new WebClient();

                wc.DownloadFile(url, tmpFileLocation + imgCt.ToString() + "." + frmt);
                return ++imgCt;
            }
            else
                return imgCt;
        }

        private string GetSubstringBetweenTags(string searchString, string startTagValue, string endTagValue)
        {
            int a = searchString.IndexOf(startTagValue);
            int b = searchString.IndexOf(endTagValue, a + startTagValue.Length) + 1;
            if (a > 0 && b > 0)
                return searchString.Substring(a, b - a).Replace(startTagValue, string.Empty).Replace(endTagValue, string.Empty);
            else
                return null;
        }

        private bool IsImageTag(string tag)
        {
            return tag.Length > 3 ? tag.Substring(0, 3) == "img" : false;
        }

        private bool IsValidURL(string uri)
        {
            Uri uriResult;
            return Uri.TryCreate(uri, UriKind.Absolute, out uriResult);
        }

        #endregion
    }
}
