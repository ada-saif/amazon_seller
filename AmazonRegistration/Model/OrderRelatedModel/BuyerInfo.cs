using AlphaUtil.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonSellerApi.Model.OrderRelatedModel
{
    public class BuyerInfo : SQLTable
    {
        [Column("amazon_order_id")]
        public string AmazonOrderId { get; set; }
        [Column("buyer_email")]
        public string BuyerEmail { get; set; }
        [Column("buyer_name")]
        public string BuyerName { get; set; }
        [Column("buyer_country")]
        public string BuyerCountry { get; set; }
        [Column("buyer_tax_info")]
        public string BuyerTaxInfo { get; set; }
        [Column("purchase_order_number")]
        public string PurchaseOrderNumber { get; set; }

    }
}
