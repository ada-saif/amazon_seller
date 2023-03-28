namespace AmazonRegistration.Model
{
    public class Profile
    {
        public int p_id { get; set; }
        public int user_id { get; set; }
        public string business { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string amazon_user_id { get; set; }
        public string amazon_user_name { get; set; }
        public string amazon_user_email { get; set; }

    }
    public class returnProfileData
    {
        public int? p_id { get; set; }
        public int? user_id { get; set; }
        public string? sub_name { get; set; }
        public string? region { get; set; }
        public string? amazon_user_id { get; set; }
    }

    public class UserProfileResponse
        {
            public string User_id { get; set; }
            public string name { get; set; }
            public string email { get; set; }

        }
}
