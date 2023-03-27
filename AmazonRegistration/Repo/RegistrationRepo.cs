using AmazonRegistration.Interface;
using AmazonRegistration.LoggedInUser;
using AmazonRegistration.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;
namespace AmazonRegistration.Repo
{
    public class RegistrationRepo : IRegistrationInterface
    {
        private readonly ITokenService _tokenService;
        private readonly IProfile _profile;
        private readonly LoggedIn loggedInUser;

        private readonly ApplicationDbContext db;

        public RegistrationRepo(ApplicationDbContext db, ITokenService _tokenService, IProfile profile, IHttpContextAccessor contextAccessor)
        {
            this.db = db;
            this._tokenService = _tokenService;
            this._profile = profile;
            this.loggedInUser = log.GetLoggedInUser(contextAccessor);

        }

        public Response UserLogin(Login login)
        {
            var response = new Response();
            if (login == null)
            {
                response.Message = "Invalid client request";
                return response;
            }
            if (login.MobileNumber.Length < 2 || login.MobileNumber.Length > 20) { response.Message = "Enter min 2 and max 20 length"; return response; }
            if (login.Password.Length < 8 || login.Password.Length > 25) { response.Message = "Enter min 8 and max 25 length"; return response; }
            var user = this.LoginCheck(login);
            var count = _profile.GetUserCount(user.id);
            var claim = new Claim("UserCount", count.ToString());
            if (user == null)
            {
                response.Message = "Incorrect mobileNumber and Password";
                return response;
            }
            var claims = new List<Claim>();
            claims.Add(claim);
            foreach (var prop in user.GetType().GetProperties())
            {
                var value = prop.GetValue(user);
                if (value != null)
                {
                    claims.Add(new Claim(prop.Name.ToString(), value.ToString()));
                }
            }
            var data = _tokenService.GenerateAndSaveToken(claims, user);
            if (data != null)
            {
                response.Status = true;
                response.Message = "login successfully";
                response.ResponseObject = data;
                return response;
            }
            else
            {
                response.Message = "data not found";
                return response;
            }
        }

        //public Response UserRegistration(RegistrationModel userModel)
        //{

        //    Response response = new Response();
        //    if (userModel == null)
        //    {
        //        response.Status = false;
        //        response.Message = "Please Provide The Data";
        //        return response;
        //    }
        //    var checkMobileNumber = db.tbl_user_registration.FirstOrDefault(a => a.user_mobile_no == userModel.user_mobile_no);
        //    if (checkMobileNumber != null) { response.Message = "Mobile Number already exists"; return response; }

        //    var CheckUsername = db.tbl_user_registration.FirstOrDefault(a => a.user_name == userModel.user_name);
        //    if (CheckUsername != null)
        //    {
        //        response.Message = "Username already exists";
        //        return response;
        //    }
        //    if (userModel.user_name.Length < 2 || userModel.user_name.Length > 20) { response.Message = "Enter min 2 and max 20 length"; return response; }
        //    if (userModel.user_password.Length < 8 || userModel.user_password.Length > 25) { response.Message = "Enter min 8 and max 25 length"; return response; }
        //    RegistrationModel obj = new RegistrationModel();
        //    obj.user_name = userModel.user_name;
        //    obj.user_mobile_no = userModel.user_mobile_no;
        //    obj.user_email = userModel.user_email;
        //    obj.user_password = Salt(userModel.user_password);
        //    obj.is_active = userModel.is_active;
        //    db.tbl_user_registration.Add(obj);
        //    int res = db.SaveChanges();
        //    if (res > 0)
        //    {
        //        response.Status = true;
        //        response.Message = "data add succeessfully";
        //        response.ResponseObject = obj.WithoutPassword();
        //        return response;
        //    }
        //    else
        //    {
        //        response.Status = false;
        //        response.Message = "no data add";
        //        return response;

        //    }
        //}
        public string Salt(string Original)
        {
            var hash = "ajhsjfhkasfuasfasfho";
            var salt = Convert.FromBase64String(hash);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: Original,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));
            return hashed;
        }

        public async Task<Response> GenerateAccessTokenss(authModel user)
        {
            Response response = new Response();
            try
            {
                RestClient rc = new RestClient();
                RestRequest rr = new RestRequest(Config.D.credentials.token_url, Method.Post);
                rr.AddJsonBody(new
                {
                    grant_type = "authorization_code",
                    code = user.auth_code,
                    client_id = Config.D.credentials.client_id,
                    client_secret = Config.D.credentials.client_secret,
                    redirect_url = Config.D.credentials.redirect_url,
                });
                var resp = rc.ExecutePost(rr);
                var data = JsonConvert.DeserializeObject<AccessTokenResponse>(resp.Content);
                if (resp == null) { response.Message = "Failed to generate token"; return response; }
                var ad = new AccessTokenResponse
                {
                    access_token = data.access_token,
                    refresh_token = data.refresh_token,
                    expires_in = data.expires_in,
                };
                var access = db.tbl_user_profile.FirstOrDefault(a => a.user_id == user.user_id);
                if (access.a_expires_in < DateTime.Now.AddMinutes(1))
                {
                    SellerDetail seller = new SellerDetail();
                    seller.access_token = ad.access_token;
                    seller.a_expires_in = Convert.ToDateTime(ad.expires_in);
                    db.Entry(access).CurrentValues.SetValues(access);
                    db.SaveChanges();
                }
                if (access == null)
                {
                    SellerDetail seller = new SellerDetail();
                    seller.user_id = user.user_id;
                    seller.access_token = ad.access_token;
                    seller.refresh_token = ad.refresh_token;
                    seller.s_expires_in = Convert.ToDateTime(ad.expires_in);
                    db.SaveChanges();
                }
                var user_profile_data = _profile.GetUserProfile(ad.access_token);
                if (user_profile_data != null)
                {response.Status = true; response.Message = "Data  get successfully"; response.ResponseObject = user_profile_data; return response;}
                else
                {response.Message = "No User Detail Found"; return response;}
            }
            catch (Exception ex)
            {response.Message = ex.Message;return response;}
        }
        public RegistrationModel LoginCheck(Login login)
        {

            if (login != null)
            {
                var obj = db.tbl_user_registration.FirstOrDefault(a => a.user_mobile_no == login.MobileNumber && a.user_password == this.Salt(login.Password));
                if (obj != null)
                { return obj.WithoutPassword(); }
                else { return null; }
            }
            else { return null; }



        }

        public Response UserRegistration(RegistrationModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}

