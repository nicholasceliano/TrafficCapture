using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TrafficCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            //new ImageTraffic().DownloadImagesFromURL("http://www.sbnation.com/2014/4/11/5576762/this-week-in-gifs-damn-it-astros", ImageTraffic.ImgFormat.gif);

            new ImageTraffic().GetGIFDuration();
        }
    }
}
