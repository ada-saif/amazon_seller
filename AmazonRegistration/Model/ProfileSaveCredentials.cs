using System.ComponentModel.DataAnnotations;

namespace AmazonRegistration.Model
{
    public class SellerDetail
    {
        [Key]
        public int? p_id { get; set; }
        public int? user_id { get; set; }
        public string amazon_user_id { get; set; }
        public string amazon_user_name { get; set; }    
        public string amazon_user_email { get; set; }
        public DateTime? created_on { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public int? updated_by { get; set; }
    }
}
