using AmazonRegistration.Interface;
using AmazonRegistration.Model;
using RestSharp;

namespace AmazonRegistration.Repo
{
    public class GanrateAccessToken : IGanrateAccessToken
    {
        private readonly ApplicationDbContext _db;

        public GanrateAccessToken(ApplicationDbContext _db)
        {
            this._db = _db;
        }
            private  async Task<AccessTokenData> GenerateNewToken( int p_id)
            {
                RestClient rc = new RestClient();
                RestRequest rr = new RestRequest(Config.D.token_url, Method.Post);
                rr.AddJsonBody(new
                {
                    grant_type = "refresh_token",
                    refresh_token = Config.D.credentials.refresh_token,
                    client_id = Config.D.credentials.client_id,
                    client_secret = Config.D.credentials.client_secret
                });
                var resp = await rc.ExecutePostAsync<AccessTokenResponse>(rr);
                var ad = new AccessTokenData
                {
                    access_token = resp.Data.access_token,
                    expiry = DateTime.Now.AddSeconds(resp.Data.expires_in),
                };
            var User_detail = _db.tbl_access_token.FirstOrDefault(a => a.subsription_id == p_id);
            if (User_detail == null) { return null; }
            User_detail.access_token = ad.access_token;
            User_detail.expires_on = ad.expiry;
            _db.Entry(User_detail).CurrentValues.SetValues(User_detail);
            if (_db.SaveChanges() > 0)
            {
                return ad;
            }
            else
            {
                return null;
            }
            }

            private AccessTokenData? GetTokenDataFromFile(int p_id)
            {
                try
                {
                var data = _db.tbl_access_token.FirstOrDefault(a => a.subsription_id == p_id);
                var a = new AccessTokenData
                 {
                    access_token=data.access_token,
                    expiry=Convert.ToDateTime( data.expires_on),
                };
                return a;

            }
            catch (Exception)
                {
                    return null;
                }
            }

            public async Task<AccessTokenData> GetToken(int p_id)
            {
                var tokenData = GetTokenDataFromFile(p_id);
                if (tokenData == null || tokenData.expiry < DateTime.Now.AddMinutes(1))
                    tokenData = await GenerateNewToken( p_id);
                return tokenData;
            }
        }
    }

