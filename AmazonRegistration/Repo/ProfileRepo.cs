using AmazonRegistration.Interface;
using AmazonRegistration.LoggedInUser;
using AmazonRegistration.Model;
using Newtonsoft.Json;
using Npgsql;
using RestSharp;

namespace AmazonRegistration.Repo
{
    public class ProfileRepo : IProfile
    {
        private readonly ApplicationDbContext db;
        private readonly IConfiguration _config;
        private readonly LoggedIn loggedInUser;


        public ProfileRepo(ApplicationDbContext db, IConfiguration config, IHttpContextAccessor contextAccessor)
        {
            this.db = db;
           _config = config;
            this.loggedInUser = log.GetLoggedInUser(contextAccessor);
        }
        public UserProfileResponse GetUserProfile(string AccessToken)
        {
            try
            {
                Response response = new Response();
                RestClient rc = new RestClient();

                RestRequest rr = new RestRequest(Config.D.credentials.profile_url, Method.Get);
                rr.AddHeader("Authorization", "Bearer " + AccessToken);
                var resp = rc.ExecuteGet(rr);

                var data = JsonConvert.DeserializeObject<UserProfileResponse>(resp.Content);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Response SaveCredentials(SellerDetail sellerDetail)
        {
            Response response = new Response();
            if (sellerDetail == null) { response.Message = "no detail found";return response; }
            var getUser = db.tbl_user_profile.FirstOrDefault(a => a.amazon_user_id == sellerDetail.amazon_user_id);
            if (getUser != null) { response.Message = "This user already Registered"; return response; }
            try
            {
                SellerDetail seller = new SellerDetail();
                seller.client_id = Config.D.credentials.client_id;
                seller.client_secret = Config.D.credentials.client_secret;
                seller.user_id = Convert.ToInt32(sellerDetail.user_id);
                seller.amazon_user_name = sellerDetail.amazon_user_name;
                seller.amazon_user_email = sellerDetail.amazon_user_email;
                seller.amazon_user_id = sellerDetail.amazon_user_id;
                seller.aws_access_key = sellerDetail.aws_access_key;
                seller.refresh_token = sellerDetail.refresh_token;
                seller.a_expires_in = sellerDetail.a_expires_in;
                seller.created_by = Convert.ToInt32(sellerDetail.user_id);
                seller.created_on = DateTime.UtcNow;
                seller.updated_by = Convert.ToInt32(sellerDetail.user_id);
                seller.updated_on = DateTime.UtcNow;
                db.tbl_user_profile.Add(seller);
                int res = db.SaveChanges();
                if (res > 0)
                {
                    response.Status = true;
                    response.Message = "Data added succeessfully";
                    response.ResponseObject = res;
                    return response;
                }
                else
                {
                    response.Message = "Failed to add data";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public int? GetUserCount(int? userId)
        {
            Response response = new Response();
            if (userId == null||userId==0) { return 0; }
            var data = db.tbl_user_profile.Where(a => a.user_id == userId).ToList();
            if (data.Count != null && data.Count > 0)
            {
                var UserCount = data.Count();
                return UserCount;
            }
            else
            {
                return 0;
            }


        }
        public Response GetUserDetail(int UserId)
        {
            Response response = new Response();
            if (UserId == null || UserId == 0) { response.Message = "please enter the userId"; return response; }
            var sql = "select p_id,user_id,business,country,state,city,amazon_user_id,amazon_user_name,amazon_user_email from tbl_user_profile where user_id='" + UserId + "'";
            var connectionString = _config.GetConnectionString("Amazon");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader? reader = command.ExecuteReader())
                    {
                        var ProfileList = new List<Profile>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var List = new Profile
                                {
                                    p_id= reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                    user_id = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                    business = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    country = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    state = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    city = reader.IsDBNull(5) ? null: reader.GetString(5),
                                    amazon_user_id = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    amazon_user_name = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    amazon_user_email = reader.IsDBNull(8) ? null: reader.GetString(8),

                                };
                                ProfileList.Add(List);
                            }

                        }
                        if(ProfileList.Count > 0)
                        {
                            response.Status = true;
                            response.Message = "data get successfully";
                            response.ResponseObject = ProfileList;
                            return response;
                        }
                        else
                        {
                            response.Message = "no data found";
                            response.ResponseObject = ProfileList;
                            return response;
                        }
                    }

                }



            }
        }
        
    }
        }
