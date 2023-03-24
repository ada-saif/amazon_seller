using System.ComponentModel.DataAnnotations;

namespace AmazonRegistration.Model
{
    public class SellerDetail
    {
        [Key]
        public int? p_id { get; set; }
        public int? user_id { get; set; }
        public string business { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string amazon_user_id { get; set; }
        public string amazon_user_name { get; set; }
        public string amazon_user_email { get; set; }
        public string? client_id { get; set; }
        public string? client_secret { get; set; }
        public string? aws_access_key { get; set; }
        public string? refresh_token { get; set; }
        public string? access_token { get; set; }
        public DateTime a_expires_in { get; set; }
        public string session_token { get; set; }
        public DateTime s_expires_in { get; set; }
        public DateTime? created_on { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public int? updated_by { get; set; }
    }
}
