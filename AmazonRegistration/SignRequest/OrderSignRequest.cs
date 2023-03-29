using AlphaUtil;
using Amazon.Runtime;
using Amazon.Util;
using AmazonRegistration.Model;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace AmazonSellerApi.SignRequest
{
    public class OrderSignRequest 
    {
        public static string CanonicalRequest { get; set; }
        public static string Signature { get; set; }

        public static RestRequest SignRequest(Method method, string endPoint, string queryString, string payLoad, Dictionary<string, string>? headers, STSTokenData sts)
        {
            var ts = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ");
            if (headers == null) headers = new Dictionary<string, string>();
            headers.Add("host", Config.D.host);
            headers.Add("x-amz-date", ts);
            headers.Add("x-amz-security-token", sts.session_token);
            var cleanedHeaders = CleanHeaders(headers);
            string canonicalReq = "" +
                 method.ToString().ToUpper().Trim() + "\n" +
                 endPoint.ToLower().Trim() + "\n" +
                 queryString.Trim() + "\n" +
                 string.Join('\n', cleanedHeaders.OrderBy(q => q.Key).Select(q => q.Key.ToLower() + ":" + q.Value)) + "\n\n" +
                 string.Join(';', cleanedHeaders.OrderBy(q => q.Key).Select(q => q.Key.ToLower())) + "\n" +
             payLoad.A_Hash_SHA256().ToLower();

            CanonicalRequest = canonicalReq;

            string cReqHash = canonicalReq.A_Hash_SHA256().ToLower();

            string sStr = "" +
                "AWS4-HMAC-SHA256" + "\n" +
                ts + "\n" +
                ts.Split('T')[0] + "/" + Config.D.region + "/" + "execute-api" + "/" + "aws4_request" + "\n" +
                cReqHash;

            Signature = sStr;

            var kDate = GetHash("AWS4" + sts.secret_key, ts.Split('T')[0]);
            var kRegion = GetHash(kDate, Config.D.region);
            var kService = GetHash(kRegion, "execute-api");
            var kSigning = GetHash(kService, "aws4_request");
            var signature = GetHash(kSigning, sStr);
            var signedStr = ByteArrayToString(signature);
            RestRequest rr = new RestRequest("https://" + Config.D.host + endPoint + "?" + queryString, method);
            rr.AddHeaders(headers);
            rr.AddHeader("authorization", $@"AWS4-HMAC-SHA256 Credential={sts.access_key}/{ts.Split('T')[0]}/{Config.D.region}/execute-api/aws4_request, SignedHeaders={string.Join(';', cleanedHeaders.OrderBy(q => q.Key).Select(q => q.Key.ToLower()))}, Signature={signedStr}");

            return rr;
        }
        public static RestRequest SignRequestForOrderItem(Method method, string endPoint, string queryString, string payLoad, Dictionary<string, string>? headers, STSTokenData sts)
        {
            var ts = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ");
            if (headers == null) headers = new Dictionary<string, string>();
            headers.Add("host", Config.D.host);
            headers.Add("x-amz-date", ts);
            headers.Add("x-amz-security-token", sts.session_token);
            var cleanedHeaders = CleanHeaders(headers);

            string canonicalReq = "" +
                 method.ToString().ToUpper().Trim() + "\n" +
               //  endPoint.ToLower().Trim() + "\n" +
               endPoint.ToLower().Trim() +
                 queryString.Trim() + "\n\n" +
                 string.Join('\n', cleanedHeaders.OrderBy(q => q.Key).Select(q => q.Key.ToLower() + ":" + q.Value)) + "\n\n" +
                 string.Join(';', cleanedHeaders.OrderBy(q => q.Key).Select(q => q.Key.ToLower())) + "\n" +
                 payLoad.A_Hash_SHA256().ToLower();
            CanonicalRequest = canonicalReq;

            string cReqHash = canonicalReq.A_Hash_SHA256().ToLower();

            string sStr = "" +
                "AWS4-HMAC-SHA256" + "\n" +
                ts + "\n" +
                ts.Split('T')[0] + "/" + Config.D.region + "/" + "execute-api" + "/" + "aws4_request" + "\n" +
                cReqHash;

            Signature = sStr;

            var kDate = GetHash("AWS4" + sts.secret_key, ts.Split('T')[0]);
            var kRegion = GetHash(kDate, Config.D.region);
            var kService = GetHash(kRegion, "execute-api");
            var kSigning = GetHash(kService, "aws4_request");
            var signature = GetHash(kSigning, sStr);
            var signedStr = ByteArrayToString(signature);
            RestRequest rr = new RestRequest("https://" + Config.D.host + endPoint + queryString, method);
            rr.AddHeaders(headers);
            rr.AddHeader("authorization", $@"AWS4-HMAC-SHA256 Credential={sts.access_key}/{ts.Split('T')[0]}/{Config.D.region}/execute-api/aws4_request, SignedHeaders={string.Join(';', cleanedHeaders.OrderBy(q => q.Key).Select(q => q.Key.ToLower()))}, Signature={signedStr}");

            return rr;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString().ToLower();
        }

        public static Dictionary<string, string> CleanHeaders(Dictionary<string, string> headers)
        {
            headers.Remove("x-amz-access-token");
            return headers;
        }

        public static byte[] GetHash(byte[] data, byte[] key) => CryptoUtilFactory.CryptoInstance.HMACSignBinary(key, data, SigningAlgorithm.HmacSHA256);
        public static byte[] GetHash(byte[] data, string key) => GetHash(data, Encoding.UTF8.GetBytes(key));
        public static byte[] GetHash(string data, string key) => GetHash(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));

    }



}

