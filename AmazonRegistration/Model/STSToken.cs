using System.ComponentModel.DataAnnotations;

namespace AmazonRegistration.Model
{
    public class AccessTokenResponse
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
    }

    public class AccessTokenData
    {
        public string access_token { get; set; }
        public DateTime expiry { get; set; }
    }

    public class STSTokenData
    {
        [Key]
        public int? id { get; set; }
        public string access_key { get; set; }
        public int user_id { get; set; }
        public string secret_key { get; set; }
        public string session_token { get; set; }
        public DateTime expires_on { get; set; }
    }

}
