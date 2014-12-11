using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using DelphiCTCombinaiton.Models;

namespace DelphiCTCombinaiton
{
    public class TokenHelper
    {
        //public static AuthToken GetToken(string guidsting, string name = "Neusoft_Tenant", string pwd = "Neusoft2014", string token_type = "Basic", string path = @"/api/v1/token ")
        //public static AuthToken GetToken(string guidsting, string name = "0000-619F", string pwd = "Neusoft2014", string token_type = "Basic", string path = @"/api/v1/token ")
        public static AuthToken GetToken(string guidsting, string name, string pwd, string token_type = "Basic", string path = @"/api/v1/token ")    
    {
            string auth = GetBase64String(guidsting + ":" + guidsting);
            string postcontent = string.Format("grant_type=password&username={0}&password={1}", name, pwd);
            HttpItem hi = new HttpItem() { Authorization = token_type + " " + auth, RelatedAddress = path, RequestMethod = RequestMethod.Post, HttpMsgBodyContent = postcontent, Content_Type = "application/x-www-form-urlencoded" };
            var result = new HTTPHelper(hi).HttpHelperMethod().Result;
            var token = JsonHelper.JsonDeserialize<AuthToken>(result);
            return token;
        }

        private static string GetBase64String(string code)
        {
            string encode;

            byte[] bytes = Encoding.Default.GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }

        private static string GetMd5Encrypt(string code)
        {
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(code);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

    }

    public static class TokenSeeker
    {
        public static AuthToken CurrentToken { get; set; }
    }
}
