using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using AmazonRegistration.Interface;
using AmazonRegistration.Model;
using Newtonsoft.Json;

namespace AmazonRegistration.Repo
{
    public class STSTokenGenerate : ISTSToken
    {

        private readonly ApplicationDbContext db;
        private readonly IConfiguration _config;
        public STSTokenGenerate(ApplicationDbContext db, IConfiguration config)
        {
            this.db = db;
            _config = config;
        }

        public async Task<STSTokenData> GenerateNewToken(int p_id)
        {
            var client = new AmazonSecurityTokenServiceClient(Config.D.credentials.aws_access_key, Config.D.credentials.aws_secret_key, Amazon.RegionEndpoint.EUWest1);

            var callerIdRequest = new GetCallerIdentityRequest();
            var caller = client.GetCallerIdentityAsync(callerIdRequest);
            var assumeRoleReq = new AssumeRoleRequest()
            {
                DurationSeconds = 3600,
                RoleSessionName = "Session",
                RoleArn = Config.D.credentials.iam_arn
            };

            var assumeRoleRes = await client.AssumeRoleAsync(assumeRoleReq);
            var sd = new STSTokenData
            {
                access_key = assumeRoleRes.Credentials.AccessKeyId,
                secret_key = assumeRoleRes.Credentials.SecretAccessKey,
                session_token = assumeRoleRes.Credentials.SessionToken,
                expiry = DateTime.Now.AddSeconds(3600),
            };
            var User_detail = db.tbl_user_profile.FirstOrDefault(a => a.p_id == p_id);
            if (User_detail == null) { return null; }
            User_detail.aws_access_key = sd.access_key;
            User_detail.client_secret = sd.secret_key;
            User_detail.s_expires_in = sd.expiry;
            User_detail.session_token = sd.session_token;
            db.Entry(User_detail).CurrentValues.SetValues(User_detail);
            if (db.SaveChanges() > 0)
            {
                return sd;
            }
            else
            {
                return null;
            }
        }

        private STSTokenData? GetTokenDataFromFile(int p_id)
        {
            try
            {
                var data = db.tbl_user_profile.FirstOrDefault(a => a.p_id == p_id);
                var a = new STSTokenData
                {
                    secret_key = data.session_token,
                    expiry = data.s_expires_in,
                };
                return a;

            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<STSTokenData> GetToken(int p_id)
        {
            var tokenData = GetTokenDataFromFile(p_id);
            if (tokenData == null || tokenData.expiry < DateTime.Now.AddMinutes(1))
                tokenData = await GenerateNewToken(p_id);
            return tokenData;
        }

    }
}
