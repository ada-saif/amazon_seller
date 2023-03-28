using System.ComponentModel.DataAnnotations;

namespace AmazonSellerApi.Model
{
    public class AccessTokenModel
    {
        [Key]
        public int? id { get; set; }
        public int subsription_id { get; set; }
        public string access_token { get; set; }
        public DateTime expires_on { get; set; }
    }
}
