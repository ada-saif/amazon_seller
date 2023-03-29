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


        public ProfileRepo(ApplicationDbContext db, IConfiguration config, IHttpContextAccessor contextAccessor)
        {
            this.db = db;
           _config = config;
        }
        public UserProfileResponse GetUserProfile(string AccessToken,int user_id)
        {
            try
            {
                Response response = new Response();
                RestClient rc = new RestClient();

                RestRequest rr = new RestRequest(Config.D.credentials.profile_url, Method.Get);
                rr.AddHeader("Authorization", "Bearer " + AccessToken);
                var resp = rc.ExecuteGet(rr);

                var data = JsonConvert.DeserializeObject<UserProfileResponse>(resp.Content);
                if(data != null)
                {
                    Profile pro = new Profile();
                    pro.user_id = Convert.ToInt32(user_id);
                    pro.amazon_user_email = data.email;
                    pro.amazon_user_id = data.User_id;
                    pro.amazon_user_name = data.name;
                    pro.created_by = user_id;
                    pro.created_on = DateTime.Now;
                    pro.updated_by = user_id;
                    pro.updated_on = DateTime.Now;
                    db.tbl_user_profile.Add(pro);
                    db.SaveChanges();
                }
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public Response SaveCredentials(SellerDetail sellerDetail)
        //{
        //    Response response = new Response();
        //    if (sellerDetail == null) { response.Message = "no detail found";return response; }
        //    var getUser = db.tbl_user_profile.FirstOrDefault(a => a.amazon_user_id == sellerDetail.amazon_user_id);
        //    if (getUser != null) { response.Message = "This user already Registered"; return response; }
        //    try
        //    {
        //        SellerDetail seller = new SellerDetail();
        //        seller.client_id = Config.D.credentials.client_id;
        //        seller.client_secret = Config.D.credentials.client_secret;
        //        seller.user_id = Convert.ToInt32(sellerDetail.user_id);
        //        seller.amazon_user_name = sellerDetail.amazon_user_name;
        //        seller.amazon_user_email = sellerDetail.amazon_user_email;
        //        seller.amazon_user_id = sellerDetail.amazon_user_id;
        //        seller.aws_access_key = sellerDetail.aws_access_key;
        //        seller.refresh_token = sellerDetail.refresh_token;
        //        seller.a_expires_in = sellerDetail.a_expires_in;
        //        seller.created_by = Convert.ToInt32(sellerDetail.user_id);
        //        seller.created_on = DateTime.UtcNow;
        //        seller.updated_by = Convert.ToInt32(sellerDetail.user_id);
        //        seller.updated_on = DateTime.UtcNow;
        //        db.tbl_user_profile.Add(seller);
        //        int res = db.SaveChanges();
        //        if (res > 0)
        //        {
        //            response.Status = true;
        //            response.Message = "Data added succeessfully";
        //            response.ResponseObject = res;
        //            return response;
        //        }
        //        else
        //        {
        //            response.Message = "Failed to add data";
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message;
        //        return response;
        //    }
        //}

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
            var sql = "select ts.user_id,ts.region,ts.sub_name from tbl_user_subsription ts where user_id='" + UserId + "'";
            var connectionString = _config.GetConnectionString("Amazon");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader? reader = command.ExecuteReader())
                    {
                        var ProfileList = new List<returnProfileData>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var List = new returnProfileData
                                {
                                    user_id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                    region= reader.IsDBNull(1) ? null : reader.GetString(1),
                                    sub_name= reader.IsDBNull(2) ? null : reader.GetString(2),
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
