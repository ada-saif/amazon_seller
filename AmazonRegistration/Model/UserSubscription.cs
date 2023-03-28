namespace AmazonSellerApi.Model
{
    
        public class UserSubscription
        {
            public int? id { get; set; }
            public int? user_id { get; set; }
            public string sub_name { get; set; }
            public string region { get; set; }
            public string? auth_key { get; set; }
            public DateTime? auth_on { get; set; }
            public string? spapi_oauth_code { get; set; }
            public string? mws_auth_token { get; set; }
            public string? selling_partner_id { get; set; }
            public string? refresh_token { get; set; }
            public bool? is_active { get; set; }
            public DateTime? expires_on { get; set; }
        }

    }

