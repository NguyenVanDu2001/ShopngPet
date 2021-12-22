using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ShopThoiTrang.GiaoHangTietKiem
{
    public class SendRequest
    {
        public SendRequest() { }
        public static string sendTransportRequest(string endpoint, string postJsonString)
        {

            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endpoint);
                var postData = postJsonString;
                var data = Encoding.UTF8.GetBytes(postData);
                httpWReq.ProtocolVersion = HttpVersion.Version11;
                httpWReq.ContentLength = data.Length;
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json";
                httpWReq.Headers.Add("Token", "B40E619F7C3cfeCc13402B0EAfB7496068c3E188");
                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

                string jsonresponse = "";

                using (var reader = new StreamReader(response.GetResponseStream()))
                {

                    string temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                }
                return jsonresponse;

            }
            catch (WebException e)
            {
                return e.Message;
            }
        }
    }
}