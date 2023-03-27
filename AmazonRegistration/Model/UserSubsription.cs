namespace AmazonSellerApi.Model
{
    
        public class UserSubscription
        {
            public int? Id { get; set; }
            public int? UserId { get; set; }
            public string SubName { get; set; }
            public string Region { get; set; }
            public string? AuthKey { get; set; }
            public DateTime? AuthOn { get; set; }
            public string? SpapiOauthCode { get; set; }
            public string? MwsAuthToken { get; set; }
            public string? SellingPartnerId { get; set; }
            public string? RefreshToken { get; set; }
            public bool? IsActive { get; set; }
            public DateTime? ExpiresOn { get; set; }
        }

    }

