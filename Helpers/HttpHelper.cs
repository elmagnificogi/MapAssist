using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace MapAssist.Helpers
{
    static class HttpHelper
    {
        static string http = "http";
        static string host = "dctrack.elmagnifico.tech";
        static string port = "8000";

        //static string http = "http";
        //static string host = "127.0.0.1";
        //static string port = "8080";

        public static string GetDC()
        {
            try
            {
                var Url = http + "://" + host + ":" + port;
                var retString = string.Empty;
                var request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.ContentType = "application/json";

                var response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                var streamReader = new StreamReader(myResponseStream);
                retString = streamReader.ReadToEnd();
                streamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
