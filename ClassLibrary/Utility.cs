using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ReactCMS.ClassLibrary
{
    public class Utility
    {
      
        /// <summary>
        /// 取得Web.Config值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfigValue(string key)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json");
            IConfiguration config = builder.Build();

            return config[key];

            //string val = WebConfigurationManager.AppSettings[key];
            //return val;
        }
        public string GetQuerystringValue(string queryString, string key)
        {
            if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?"))
                queryString = queryString.Replace("?", string.Empty);
            var nvc = HttpUtility.ParseQueryString(queryString);
            string ret = nvc.Get(key);
            return ret;
        }

        public string GetRandomString(int Length)
        {
            Random rnd = new Random();
            StringBuilder RandomString = new StringBuilder();
            for (int i = 1; i <= Length; i++)
            {
                RandomString.Append(rnd.Next(0, 9).ToString());
            }
            return RandomString.ToString();
        }
        public string DecryptAES256(string toDecrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.KeySize = 256;
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);

        }
        public string EncryptAES256(string toEncrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);


            RijndaelManaged rDel = new RijndaelManaged();
            rDel.KeySize = 256;
            rDel.Key = keyArray;
            rDel.IV = ivArray;  // 初始化向量 initialization vector (IV)
            rDel.Mode = CipherMode.CBC; // 密碼分組連結（CBC，Cipher-block chaining）模式
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }
        //加密
        public string Base64Encode(string AStr)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(AStr));
        }

        //解密
        public string Base64Decode(string ABase64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(ABase64));
        }
        public string SHA256Encode(string Data)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();//建立一個SHA256
            byte[] source = Encoding.Default.GetBytes(Data);//將字串轉為Byte[]
            byte[] crypto = sha256.ComputeHash(source);//進行SHA256加密
            string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
            return result;
        }
        public string SHA512Encode(string Data)
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();//建立一個SHA512
            byte[] source = Encoding.Default.GetBytes(Data);//將字串轉為Byte[]
            byte[] crypto = sha512.ComputeHash(source);//進行SHA512加密
            string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
            return result;
        }

        public string ObjectToJson(object Source)
        {
            //return  JsonConvert.SerializeObject(Source).ToString().Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
            return JsonConvert.SerializeObject(Source).ToString();
        }
        public int GetTimeStamp(int AddMin = 0)
        {
            DateTime nw = DateTime.UtcNow;

            DateTime gtm = new DateTime(1970, 1, 1);//宣告一個GTM時間出來
            DateTime utc = nw.AddMinutes(AddMin);//宣告一個目前的時間
            int timeStamp = Convert.ToInt32(((TimeSpan)utc.Subtract(gtm)).TotalSeconds);
            return timeStamp;
        }
        public void LoadHeaderContent(System.Net.Http.Headers.HttpRequestHeaders _RequestHeader, ref int TimeStamp, ref string Token)
        {
            IEnumerable<string> _TimeStamp;
            IEnumerable<string> _Token;
            IEnumerable<string> _Signature;
            _RequestHeader.TryGetValues("ts", out _TimeStamp);
            _RequestHeader.TryGetValues("t", out _Token);
            _RequestHeader.TryGetValues("s", out _Signature);
            TimeStamp = 0;
            Token = string.Empty;
            string Signature = string.Empty;
            foreach (string tmp in _TimeStamp)
                TimeStamp = int.Parse(tmp);
            foreach (string tmp in _Token)
                Token = tmp;
            foreach (string tmp in _Signature)
                Signature = tmp;
        }

        public string EncryptAES256_STD(string toEncrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);


            RijndaelManaged rDel = new RijndaelManaged();
            rDel.KeySize = 256;
            rDel.Key = keyArray;
            rDel.IV = ivArray;  // 初始化向量 initialization vector (IV)
            rDel.Mode = CipherMode.CBC; // 密碼分組連結（CBC，Cipher-block chaining）模式
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            //byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            string encrypt = string.Empty;
            //return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                // Create the streams used for encryption.
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV), CryptoStreamMode.Write))
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(cs))
                        {
                            //Write all data to the stream.
                            sw.Write(toEncrypt);
                        }

                        encrypt = Convert.ToBase64String(ms.ToArray());

                    }
                }
            }
            return encrypt;
        }
        public string EncryptSHA512(string Data)
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();//建立一個SHA512
            byte[] source = Encoding.Default.GetBytes(Data);//將字串轉為Byte[]
            byte[] crypto = sha512.ComputeHash(source);//進行SHA512加密
            string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
            return result;
        }
        public string EncryptSHA512_STD(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            try
            {
                SHA512 sha512 = new SHA512CryptoServiceProvider();
                byte[] retVal = sha512.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetSHA512HashFromString() fail,error:" + ex.Message);
            }
        }
        public string DecryptAES256_STD(string toDecrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.KeySize = 256;
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            string encrypt = string.Empty;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (System.IO.MemoryStream msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(toDecrypt)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (System.IO.StreamReader srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            encrypt = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return encrypt;

        }

        #region 呼叫API

        /// <summary>
        /// 呼叫POST(抓取預設設定)
        /// </summary>
        /// <param name="APIUrl">API網址</param>
        /// <param name="Data">傳入資料(JSON格式)</param>
        /// <returns></returns>
        public BasicResponse PostAPIBySTD(string APIUrl, string Data)
        {
            string _DataString = string.Empty;
            string Cipher_Text = string.Empty;
            int Timestamp = 0;
            string Signature = string.Empty;
            Utility utilty = new Utility();

            API_Attr Attr = new API_Attr();
            Attr.AES_KEY = GetConfigValue("API:AESKEY");
            Attr.AES_IV = GetConfigValue("API:AESIV");
            Attr.TOKEN = GetConfigValue("API:TOKEN");
            Attr.SALT_KEY = GetConfigValue("API:SALTKEY");

            //string Data = "{\"ActionType\": \"Q\",	\"RequestData\": {\"MARKNO\":\"IPSA\",\"WSNO\":\"" + account + "\",\"PWD\":\"" + password + "\"}} ";

            _DataString = Data.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");

            //資料內容加密處理
            Cipher_Text = utilty.EncryptAES256_STD(_DataString, new Utility().Base64Decode(Attr.AES_KEY), utilty.Base64Decode(Attr.AES_IV));
            //取得目前系統時間TimsStamp(附1)
            Timestamp = utilty.GetTimeStamp();

            //產生簽章
            string StrSignatureBody = string.Format("{0}{1}{2}{3}", Timestamp, Attr.TOKEN, Attr.SALT_KEY, Cipher_Text);
            Signature = utilty.EncryptSHA512_STD(StrSignatureBody);

            BasicResponse result = new BasicResponse();

            #region CallAPI
            //建立 HttpClient
            //HttpClient client = new HttpClient() { BaseAddress = new Uri(Source.APIUrl) };
            //配合SSL調整寫法
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(APIUrl);
            client.Timeout = TimeSpan.FromSeconds(240);
            if (!string.IsNullOrEmpty(Attr.AES_KEY))
            {
                client.DefaultRequestHeaders.Add("ts", Timestamp.ToString());
                client.DefaultRequestHeaders.Add("t", Attr.TOKEN);
                client.DefaultRequestHeaders.Add("s", Signature);
            }
            // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
            HttpContent contentPost = new StringContent(Data, Encoding.UTF8, "application/json");
            // 發出 post 並取得結果
            HttpResponseMessage response = client.PostAsync(APIUrl, contentPost).GetAwaiter().GetResult();
            // 將回應結果內容取出並轉為 string 再透過 linqpad 輸出
            BasicResponse js = JsonConvert.DeserializeObject<BasicResponse>(response.Content.ReadAsStringAsync().Result);
            if (js.ResponseData != null && (js.ResponseNo.Equals("0000") || js.ResponseNo.Equals("00")))
            {
                string tmpData = js.ResponseData.ToString();
                Utility uty = new Utility();
                js.ResponseData = JsonConvert.DeserializeObject(uty.DecryptAES256_STD(tmpData, uty.Base64Decode(Attr.AES_KEY), uty.Base64Decode(Attr.AES_IV)));
            }
            result = js;
            #endregion

            return result;
        }
        /// <summary>
        /// 呼叫POST(抓取預設設定)
        /// </summary>
        /// <param name="APIUrl">API網址</param>
        /// <param name="Data">傳入資料(JSON格式)</param>
        /// <param name="TimeOut">TimeOut(秒)</param>
        /// <returns></returns>
        public BasicResponse PostAPIBySSD(string APIUrl, string Data,int TimeOut=300)
        {
            string _DataString = string.Empty;
            string Cipher_Text = string.Empty;
            int Timestamp = 0;
            string Signature = string.Empty;
            Utility utilty = new Utility();

            API_Attr Attr = new API_Attr();
            Attr.AES_KEY = GetConfigValue("API:AESKEY");
            Attr.AES_IV = GetConfigValue("API:AESIV");
            Attr.TOKEN = GetConfigValue("API:TOKEN");
            Attr.SALT_KEY = GetConfigValue("API:SALTKEY");

            //string Data = "{\"ActionType\": \"Q\",	\"RequestData\": {\"MARKNO\":\"IPSA\",\"WSNO\":\"" + account + "\",\"PWD\":\"" + password + "\"}} ";

            _DataString = Data.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");

            //資料內容加密處理
            Cipher_Text = utilty.EncryptAES256_STD(_DataString, new Utility().Base64Decode(Attr.AES_KEY), utilty.Base64Decode(Attr.AES_IV));
            //取得目前系統時間TimsStamp(附1)
            Timestamp = utilty.GetTimeStamp();

            //產生簽章
            string StrSignatureBody = string.Format("{0}{1}{2}{3}", Timestamp, Attr.TOKEN, Attr.SALT_KEY, Cipher_Text);
            Signature = utilty.EncryptSHA512_STD(StrSignatureBody);

            BasicResponse result = new BasicResponse();

            #region CallAPI
            //建立 HttpClient
            //HttpClient client = new HttpClient() { BaseAddress = new Uri(Source.APIUrl) };
            //配合SSL調整寫法
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(APIUrl);
            client.Timeout = TimeSpan.FromSeconds(TimeOut);
            if (!string.IsNullOrEmpty(Attr.AES_KEY))
            {
                client.DefaultRequestHeaders.Add("ts", Timestamp.ToString());
                client.DefaultRequestHeaders.Add("t", Attr.TOKEN);
                client.DefaultRequestHeaders.Add("s", Signature);
            }
            // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
            HttpContent contentPost = new StringContent(Data, Encoding.UTF8, "application/json");
            // 發出 post 並取得結果
            HttpResponseMessage response = client.PostAsync(APIUrl, contentPost).GetAwaiter().GetResult();
            // 將回應結果內容取出並轉為 string 再透過 linqpad 輸出
            BasicResponse js = JsonConvert.DeserializeObject<BasicResponse>(response.Content.ReadAsStringAsync().Result);
            if (js.ResponseData != null && (js.ResponseNo.Equals("0000") || js.ResponseNo.Equals("00")))
            {
                string tmpData = js.ResponseData.ToString();
                Utility uty = new Utility();
                js.ResponseData = JsonConvert.DeserializeObject(uty.DecryptAES256_STD(tmpData, uty.Base64Decode(Attr.AES_KEY), uty.Base64Decode(Attr.AES_IV)));
            }
            result = js;
            #endregion

            return result;
        }

        /// <summary>
        /// 非同步呼叫(沒有回傳值)
        /// </summary>
        /// <param name="APIUrl"></param>
        /// <param name="Data"></param>
        /// <param name="TimeOut"></param>
        public async Task<BasicResponse> PostAPIAsync(string APIUrl, string Data, int TimeOut = 300)
        {
            string _DataString = string.Empty;
            string Cipher_Text = string.Empty;
            int Timestamp = 0;
            string Signature = string.Empty;
            Utility utilty = new Utility();

            API_Attr Attr = new API_Attr();
            Attr.AES_KEY = GetConfigValue("API:AESKEY");
            Attr.AES_IV = GetConfigValue("API:AESIV");
            Attr.TOKEN = GetConfigValue("API:TOKEN");
            Attr.SALT_KEY = GetConfigValue("API:SALTKEY");

            _DataString = Data.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");

            //資料內容加密處理
            Cipher_Text = utilty.EncryptAES256_STD(_DataString, new Utility().Base64Decode(Attr.AES_KEY), utilty.Base64Decode(Attr.AES_IV));
            //取得目前系統時間TimsStamp(附1)
            Timestamp = utilty.GetTimeStamp();

            //產生簽章
            string StrSignatureBody = string.Format("{0}{1}{2}{3}", Timestamp, Attr.TOKEN, Attr.SALT_KEY, Cipher_Text);
            Signature = utilty.EncryptSHA512_STD(StrSignatureBody);

            BasicResponse result = new BasicResponse();

            #region CallAPI
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(APIUrl);
            client.Timeout = TimeSpan.FromSeconds(TimeOut);
            if (!string.IsNullOrEmpty(Attr.AES_KEY))
            {
                client.DefaultRequestHeaders.Add("ts", Timestamp.ToString());
                client.DefaultRequestHeaders.Add("t", Attr.TOKEN);
                client.DefaultRequestHeaders.Add("s", Signature);
            }
            // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
            HttpContent contentPost = new StringContent(Data, Encoding.UTF8, "application/json");
            // 發出 post 並取得結果
            HttpResponseMessage response = await client.PostAsync(APIUrl, contentPost);
            BasicResponse js = JsonConvert.DeserializeObject<BasicResponse>(response.Content.ReadAsStringAsync().Result);
            if (js.ResponseData != null && (js.ResponseNo.Equals("0000") || js.ResponseNo.Equals("00")))
            {
                string tmpData = js.ResponseData.ToString();
                Utility uty = new Utility();
                js.ResponseData = JsonConvert.DeserializeObject(uty.DecryptAES256_STD(tmpData, uty.Base64Decode(Attr.AES_KEY), uty.Base64Decode(Attr.AES_IV)));
            }
            result = js;
            #endregion

            return result;

        }

        /// <summary>
        /// 呼叫API[POST]
        /// </summary>
        /// <param name="APIUrl">API網址</param>
        /// <param name="Data">傳入資料(JSON格式)</param>
        /// <param name="Attr">API相關設定(簽章...etc)</param>
        /// <returns></returns>
        public BasicResponse PostAPI(string APIUrl, string Data, API_Attr Attr = null)
        {
            string _DataString = string.Empty;
            string Cipher_Text = string.Empty;
            int Timestamp = 0;
            string Signature = string.Empty;
            Utility utilty = new Utility();

            //string Data = "{\"ActionType\": \"Q\",	\"RequestData\": {\"MARKNO\":\"IPSA\",\"WSNO\":\"" + account + "\",\"PWD\":\"" + password + "\"}} ";

            _DataString = Data.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");

            //資料內容加密處理
            Cipher_Text = utilty.EncryptAES256_STD(_DataString, new Utility().Base64Decode(Attr.AES_KEY), utilty.Base64Decode(Attr.AES_IV));
            //取得目前系統時間TimsStamp(附1)
            Timestamp = utilty.GetTimeStamp();

            //產生簽章
            string StrSignatureBody = string.Format("{0}{1}{2}{3}", Timestamp, Attr.TOKEN, Attr.SALT_KEY, Cipher_Text);
            Signature = utilty.EncryptSHA512_STD(StrSignatureBody);

            BasicResponse result = new BasicResponse();

            #region CallAPI
            //建立 HttpClient
            //HttpClient client = new HttpClient() { BaseAddress = new Uri(Source.APIUrl) };
            //配合SSL調整寫法
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(APIUrl);

            if (!string.IsNullOrEmpty(Attr.AES_KEY))
            {
                client.DefaultRequestHeaders.Add("ts", Timestamp.ToString());
                client.DefaultRequestHeaders.Add("t", Attr.TOKEN);
                client.DefaultRequestHeaders.Add("s", Signature);
            }
            // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
            HttpContent contentPost = new StringContent(Data, Encoding.UTF8, "application/json");
            // 發出 post 並取得結果
            HttpResponseMessage response = client.PostAsync(APIUrl, contentPost).GetAwaiter().GetResult();
            // 將回應結果內容取出並轉為 string 再透過 linqpad 輸出
            BasicResponse js = JsonConvert.DeserializeObject<BasicResponse>(response.Content.ReadAsStringAsync().Result);
            if (js.ResponseData != null && (js.ResponseNo.Equals("0000") || js.ResponseNo.Equals("00")))
            {
                string tmpData = js.ResponseData.ToString();
                Utility uty = new Utility();
                js.ResponseData = JsonConvert.DeserializeObject(uty.DecryptAES256_STD(tmpData, uty.Base64Decode(Attr.AES_KEY), uty.Base64Decode(Attr.AES_IV)));
            }
            result = js;
            #endregion

            return result;
        }
        #endregion

        public class RequestHeader
        {
            public string Token { get; set; }
            public string Key { get; set; }
            public string IV { get; set; }
            public string Salt_Key { get; set; }
            public string SysBraNo { get; set; }
            public string ResponseNo { get; set; }
            public string ResponseNa { get; set; }

        }
    }
    public class ResponseBody
    {
        public object ResponseData { get; set; }
    }
    public class ResponseHeader
    {
        public string ResponseNo { get; set; }
        public string ResponseNa { get; set; }
    }
    public class RequestHeader
    {
        public string ts { get; set; }
        public string t { get; set; }
        public string s { get; set; }
        public string ActionType { get; set; }
    }
    public class RequestBody
    {
        public object RequestData { get; set; }
    }
    public class SignatureBody
    {
        public int Timestamp { get; set; }
        public string Token { get; set; }
        public string Salt_Key { get; set; }
        public string Cipher_Text { get; set; }

    }

    public class BasicResponse
    {
        public string ResponseNo { get; set; }
        public string ResponseNa { get; set; }
        public object ResponseData { get; set; }
    }

    public class Request
    {
        public string ActionType { get; set; }
        public object RequestData { get; set; }
    }
    /// <summary>
    /// 呼叫API基本要素，不帶值預設抓Config
    /// </summary>
    public class API_Attr
    {
        public string TOKEN;
        public string AES_KEY;
        public string AES_IV;
        public string SALT_KEY;
    }
}
