using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AmazonRegistration.Model
{
    public class RegistrationModel
    {
        [Key]
        public int? id { get; set; }
        public string user_name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool is_active { get; set; }
        public string? otp { get; set; }
        public DateTime? otp_valid_till { get; set; } 
        public RegistrationModel WithoutPassword()
        {
            this.password = null;
            return this;
        }
    }
    public class Config
    {
        public string tmp_path { get; set; }
        public string token_url { get; set; }
        public string redirect_url { get; set; }
        public string sts_url { get; set; }
        public string region { get; set; }
        public string host { get; set; }
        public string name { get; set; }
        public string market_name { get; set; }
        public ConfigConnections connection_strings { get; set; }
        public Credentials credentials { get; set; }

        public static Config D
        {
            get
            {
                var json = System.IO.File.ReadAllText("appsettings.json");
                var config = JsonConvert.DeserializeObject<Config>(json);
                return config;
            }
        }
    }
            public class Credentials
            {
                public string refresh_token { get; set; }
                public string client_id { get; set; }
                public string client_secret { get; set; }
                public string aws_access_key { get; set; }
                public string aws_secret_key { get; set; }
                public string iam_arn { get; set; }
                public string marchant_token { get; set; }
                public string redirect_url { get; set; }
                public string token_url { get; set; }
                public string profile_url { get; set; }
            }
            public class ConfigConnections
            {
                public string db { get; set; }
            }
            public class authModel 
            {
                public string auth_code { get; set; }
                public int user_id { get; set; }

            }




        }








    



