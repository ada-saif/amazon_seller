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
        public string access_key { get; set; }
        public string secret_key { get; set; }
        public string session_token { get; set; }
        public DateTime expiry { get; set; }
    }

}
