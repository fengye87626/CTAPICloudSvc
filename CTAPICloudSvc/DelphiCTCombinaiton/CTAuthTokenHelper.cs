using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DelphiCTCombinaiton.Models;

namespace DelphiCTCombinaiton
{
    public class CTAuthTokenHelper
    {
        //public static AuthToken GetToken(string guidsting, string name = "Neusoft_Tenant", string pwd = "Neusoft2014", string token_type = "Basic", string path = @"/api/v1/token ")
        //public static async Task<CTAuthTokenResult> GetToken(string platID = "guest", string pwd = "123456")

        public static async Task<CTAuthTokenResult> GetToken(string platID, string pwd)
    {
            string auth = GetMd5Encrypt(pwd);
            var CTAuthToken = new CTAuthToken() { platId = platID, secretKey = auth };
            string postcontent = JsonHelper.ToJson(CTAuthToken);
            var result = await CTHttpHelper.CTPostContent(postcontent, @"/platAuth");
            var token = JsonHelper.JsonDeserialize<CTAuthTokenResult>(result);
            return token;
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

    public static class CTTokenSeeker
    {
        public static CTAuthTokenResult CurrentToken { get; set; }
    }
}
