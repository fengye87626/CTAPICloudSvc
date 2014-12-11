using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DelphiCTCombinaiton
{
    class JsonHelper
    {
        public static String ToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static T JsonDeserialize<T>(String jsonStr)
        {
            try
            {
                var t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);
                return t;
            }
            catch {
                return default(T);
            }
        }

        /*
        /// <summary>
        /// 将JSON数据转化为C#数据实体
        /// </summary>
        /// <param name="json">符合JSON格式的字符串</param>
        /// <returns>T类型的对象</returns>
        public static T JsonDeserialize<T>(string json)
        {
            if (json == string.Empty)
                return default(T);
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                foreach (var item in json.ToCharArray())
                {
                    Console.Write(item);
                }
                Trace.WriteLine();
                MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json.ToCharArray()));
                T obj = (T)serializer.ReadObject(ms);
                ms.Close();
                return obj;
            }
            catch
            {
                return default(T);
            }
        }
        */
    }
}
