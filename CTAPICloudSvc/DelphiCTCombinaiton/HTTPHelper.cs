using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace DelphiCTCombinaiton
{
    public class HTTPHelper : IDisposable
    {
       // const string urlbaseEndPoint = @"https://hk-uat-sharedapi.delphiconnect.com";//@"https://hk-prod-sharedapi.delphiconnect.com";

        HttpItem httpItem;

        public HTTPHelper(HttpItem item)
        {
            httpItem = item;
        }

        public async Task<string> HttpHelperMethod()
        {
            string responseresult = string.Empty;
            if (httpItem.AddressBase == null)
                httpItem.AddressBase = @"https://hk-uat-sharedapi.delphiconnect.com";
            Uri target = new Uri(httpItem.AddressBase + httpItem.RelatedAddress);
            HttpWebRequest webRequest = WebRequest.Create(target) as HttpWebRequest;
            webRequest.Method = httpItem.RequestMethod.ToString();
            webRequest.ContentType = httpItem.Content_Type;
            webRequest.Headers["Authorization"] = httpItem.Authorization;
            webRequest.AllowAutoRedirect = true;
            if (httpItem.RequestMethod == RequestMethod.Post | httpItem.RequestMethod==RequestMethod.Put)
            {
                try
                {
                    using (StreamWriter requestWriter = new StreamWriter(await webRequest.GetRequestStreamAsync()))
                    {
                      await requestWriter.WriteAsync(httpItem.HttpMsgBodyContent.ToString());
                      await requestWriter.FlushAsync();
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.StackTrace);
                    return responseresult;
                }
            }

            var result = await webRequest.GetResponseAsync().ContinueWith(GetResponseTask =>
            {
                try
                {
                    if (!GetResponseTask.IsFaulted)
                    {
                        HttpWebResponse httpWebResp = GetResponseTask.Result as HttpWebResponse;
                        //Trace.WriteLine((int)httpWebResp.StatusCode + httpWebResp.StatusDescription);
                        StreamReader reader = new StreamReader( GetResponseTask.Result.GetResponseStream());
                        if (reader != null)
                        {
                            responseresult = reader.ReadToEnd();
                            //TODO: analysis the result and put the href into the list 
                            //Trace.WriteLine(responseresult);
                        }
                    }
                    else
                    {
                        try
                        {
                            var tmp = GetResponseTask.Result as HttpWebResponse;
                        }
                        catch (Exception ex)
                        {
                            var t = ((WebException)ex.InnerException).Response as HttpWebResponse;
                            if(t!=null)
                            Trace.WriteLine(t.StatusDescription);
                            return string.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.StackTrace);
                    return string.Empty;
                }
                return responseresult;
            });
            return result;

            /*
            try
            {
                var hello = webRequest.GetResponse();
                StreamReader reader = new StreamReader(hello.GetResponseStream());
                if (reader != null)
                {
                    responseresult = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var twe = ex.Response as HttpWebResponse;
                Trace.WriteLine(twe.StatusDescription.ToString());
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return responseresult;
            */
        }

        public void Dispose()
        {
            //TODO:
        }

        public static async Task<string> InvokeTaskEngineMethod(string address, RequestMethod reqMethod, string postcontent) //InvokeTaskEngineTask
        {
            var hi = new HttpItem() { RelatedAddress = address, RequestMethod = reqMethod, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/json", HttpMsgBodyContent = postcontent };
            var t = await new HTTPHelper(hi).HttpHelperMethod().ContinueWith(hello =>
            {
                Trace.WriteLine(hello.Result);  // HttpStatusCode
                return hello.Result;
            });
            return t;
        }

        #region
        /*
        private static void GetMethod(string href)
        {
            string urlbaseEndPoint = @"http://hk-uat-sharedapi.delphiconnect.com";
            Uri target = new Uri(urlbaseEndPoint + href);
            var webquest = (WebRequest)HttpWebRequest.Create(target);
            webquest.ContentType = "application/hal+json";
            webquest.Method = "Get";
            webquest.Headers["Authorization"] = "Bearer boQtj0SCGz2GFG==";

            webquest.GetResponseAsync().ContinueWith(GetResponseTask =>
            {
                Trace.WriteLine(GetResponseTask.Status);
                try
                {
                    HttpWebResponse t = GetResponseTask.Result as HttpWebResponse;
                    Trace.WriteLine(t.StatusCode.ToString());
                    if (t.StatusCode == HttpStatusCode.OK || t.StatusCode == HttpStatusCode.Created)
                    {
                        StreamReader reader = new StreamReader(GetResponseTask.Result.GetResponseStream());
                        if (reader != null)
                        {
                            string responseresult = reader.ReadToEnd();
                            //TODO: analysis the result and put the href into the list 
                            Trace.WriteLine(responseresult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(href);
                    Trace.WriteLine(ex.Message);
                }
            });
            Trace.WriteLine(string.Format("scheduled the task {0},pending for getting the result", href));
        }

        private static void PostMethod(string href, object postContent)
        {
            string urlbaseEndPoint = @"http://hk-uat-sharedapi.delphiconnect.com";
            Uri target = new Uri(urlbaseEndPoint + href);
            WebRequest webquest = HttpWebRequest.Create(target);
            //HttpWebRequest webquest = new HttpWebRequest();
            //webquest.Accept = "application/hal+json";
            webquest.Method = "Post";
            webquest.ContentType = "application/json";
            webquest.Headers["Authorization"] = "Bearer boQtj0SCGz2GFG==";
            // webquest.Headers["Accept"] = "application/hal+json";

            using (StreamWriter requestWriter1 = new StreamWriter(webquest.GetRequestStream()))
            {
                requestWriter1.Write(postContent);
                requestWriter1.Flush();
            }

            webquest.GetResponseAsync().ContinueWith(GetResponseTask =>
            {
                Trace.WriteLine(GetResponseTask.Status);
                try
                {
                    HttpWebResponse t = GetResponseTask.Result as HttpWebResponse;
                    Trace.WriteLine(t.StatusCode.ToString());
                    if (t.StatusCode == HttpStatusCode.OK || t.StatusCode == HttpStatusCode.Created)
                    {
                        StreamReader reader = new StreamReader(GetResponseTask.Result.GetResponseStream());
                        if (reader != null)
                        {
                            string responseresult = reader.ReadToEnd();
                            //TODO: analysis the result and put the href into the list 
                            Trace.WriteLine(responseresult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(href);
                    Trace.WriteLine(ex.Message);
                }
            });

            Trace.WriteLine(string.Format("scheduled the task {0},pending for getting the result", href));
        }
        */
        /*
                 public async Task<string> HttpHelperMethodOrg()
        {
            Uri target = new Uri(urlbaseEndPoint + httpItem.RelatedAddress);
            WebRequest webquest = HttpWebRequest.Create(target);
            //HttpWebRequest webquest = new HttpWebRequest();
            //webquest.Accept = "application/hal+json";
            webquest.Method = httpItem.RequestMethod.ToString();
            webquest.ContentType = httpItem.Content_Type;
            webquest.Headers["Authorization"] = httpItem.Authorization;
            //webquest.Headers["Accept"] = "application/hal+json";
            if (httpItem.RequestMethod == RequestMethod.Post)
            {
                using (StreamWriter requestWriter1 = new StreamWriter(webquest.GetRequestStream()))
                {
                    requestWriter1.Write(httpItem.HttpMsgBodyContent);
                    requestWriter1.Flush();
                }
            }

            var temp = await webquest.GetResponseAsync().ContinueWith(GetResponseTask =>
            {
                string responseresult = "";
                //Trace.WriteLine(httpItem.RelatedAddress+": "+GetResponseTask.Status);
                try
                {
                    if (!GetResponseTask.IsFaulted)
                    {
                        HttpWebResponse httpWebResp = GetResponseTask.Result as HttpWebResponse;
                        Trace.WriteLine((int)httpWebResp.StatusCode + httpWebResp.StatusDescription);
                        StreamReader reader = new StreamReader(GetResponseTask.Result.GetResponseStream());
                        if (reader != null)
                        {
                            responseresult = reader.ReadToEnd();
                            //TODO: analysis the result and put the href into the list 
                            Trace.WriteLine(responseresult);
                        }
                    }
                    else
                    {
                        Trace.WriteLine(GetResponseTask.Exception.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(httpItem.RelatedAddress + ": " + ex.InnerException.Message);
                }
                return responseresult;
            });
            //Trace.WriteLine(string.Format("scheduled the task {0},pending for getting the result", httpItem.RelatedAddress));
            return temp;
        }
         */
        #endregion
    }
}
