using System.ComponentModel.DataAnnotations;

namespace AmazonRegistration.Model
{
    public class Profile
    {
        [Key]
        public int p_id { get; set; }
        public int? user_id { get; set; }
        public string? amazon_user_id { get; set; }
        public string? amazon_user_name { get; set; }
        public string? amazon_user_email { get; set; }
        public DateTime? created_on { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public int? updated_by { get; set; }



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
