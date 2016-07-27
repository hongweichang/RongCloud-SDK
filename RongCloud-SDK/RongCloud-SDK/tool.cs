using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace RongCloud_SDK
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class tool
    {

        /// <summary>
        /// Json序列化类
        /// </summary>
        public class Json
        {
            /// <summary>
            /// 将Object对象转成Json字符串
            /// </summary>
            /// <param name="item">Object对象</param>
            /// <returns></returns>
            public static string ToJsonData(object item)
            {
                DataContractJsonSerializer serialize = new DataContractJsonSerializer(item.GetType());
                string result = String.Empty;
                using (MemoryStream ms = new MemoryStream())
                {
                    serialize.WriteObject(ms, item);
                    ms.Position = 0;
                    using (StreamReader reader = new StreamReader(ms))
                    {
                        result = reader.ReadToEnd();
                        reader.Dispose();
                    }
                }
                return result;
            }
            /// <summary>
            /// 将Json字符串转成Object对象
            /// </summary>
            /// <typeparam name="T">对象类型</typeparam>
            /// <param name="json">Json字符串</param>
            /// <returns></returns>
            public static T DataContractJsonDeSerialize<T>(string json)
            {
                var ds = new DataContractJsonSerializer(typeof(T));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                T obj = (T)ds.ReadObject(ms);
                ms.Dispose();
                return obj;
            }
        }

        /// <summary>
        /// Http请求类
        /// </summary>
        public class Http
        {
            public class HttpHeader
            {
                public string AppKey { get; set; }
                public string Timestamp { get; set; }
                public string Signature { get; set; }
                public string rand { get; set; }
                public HttpHeader()
                {
                    this.AppKey = Model.AppKey;
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    this.Timestamp= Convert.ToInt64(ts.TotalSeconds).ToString();
                    rand = (new Random().Next()).ToString();
                    string strAlgName = HashAlgorithmNames.Sha1;
                    HashAlgorithmProvider objAlgProv = HashAlgorithmProvider.OpenAlgorithm(strAlgName);
                    CryptographicHash objHash = objAlgProv.CreateHash();
                    var strMsg = Model.AppSecret+ rand + Timestamp;
                    IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(strMsg, BinaryStringEncoding.Utf8);
                    objHash.Append(buffMsg);
                    IBuffer buffHash = objHash.GetValueAndReset();
                    Signature = CryptographicBuffer.EncodeToBase64String(buffHash);
                }
            }
            public static async Task<string> Get(string url)
            {
                var httpClient = new HttpClient();
                var header = new HttpHeader();
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
                httpClient.DefaultRequestHeaders.Add("App-Key", header.AppKey);
                httpClient.DefaultRequestHeaders.Add("Nonce", header.rand);
                httpClient.DefaultRequestHeaders.Add("Timestamp", header.Timestamp);
                httpClient.DefaultRequestHeaders.Add("Signature", header.Signature);
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(url));
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            public static async Task<string> Post(string url,Dictionary<string,string> data)
            {
                var httpClient = new HttpClient();
                var postdata = new FormUrlEncodedContent(data);
                var header = new HttpHeader();
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
                httpClient.DefaultRequestHeaders.Add("App-Key", header.AppKey);
                httpClient.DefaultRequestHeaders.Add("Nonce", header.rand);
                httpClient.DefaultRequestHeaders.Add("Timestamp", header.Timestamp);
                httpClient.DefaultRequestHeaders.Add("Signature", header.Signature);
                HttpResponseMessage response = await httpClient.PostAsync(url,postdata);
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}
