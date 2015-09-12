using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ForexHistory.Controllers
{
    public class apiController : Controller
    {
        //
        // GET: /api/

        public ActionResult Index()
        {
            return View();
        }
        public double get(string date, string cur1, string cur2)
        {
            string urlAddress="";
            if(date=="getlatest")urlAddress = "http://currencies.apps.grandtrunk.net/"+date+"/"+cur1+"/"+cur2+"";
            else urlAddress = "http://currencies.apps.grandtrunk.net/getrate/" + date + "/" + cur1 + "/" + cur2 + "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null) readStream = new StreamReader(receiveStream);
                else readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                double data = Convert.ToDouble(readStream.ReadToEnd().Replace(".", ","));
                data=Math.Round(data, 2);

                response.Close();
                readStream.Close();
                return data;
            }
            return 0.0;
        }

        public double get(string date1, string date2, string cur1, string cur2, double amount)
        {
            double first = get(date1, cur1, cur2) * amount;
            double second = get(date2, cur2, cur1) * first;
            return second;
        }

    }
}
