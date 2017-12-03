using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameAttempt
{
    public static class DataApiController<T>
    {
        public static T GetData(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
                
            request.ContentType = @"application/json";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    return (T)JsonConvert.DeserializeObject(json, typeof(T));

                    
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response != null)
                {
                    return default(T);
                }
                else
                {
                    MessageBox.Show("Error communicating with server. Please try again later.", "Server error");
                    return default(T);
                }
            }
        }

        public static bool PostData(string url, T data)
        {
            string json = (data != null) ? JsonConvert.SerializeObject(data) : "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(json);
            request.Method = "POST";
            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            try
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response != null)
                {
                    return false;
                }
                else
                {
                    MessageBox.Show("Error communicating with server. Please try again later.", "Server error");
                    return false;
                }
            }
        }
    }
}
