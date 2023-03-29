using AlphaUtil.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonSellerApi.Model.OrderRelatedModel
{
    public partial class PaymentExecutionDetailItem : SQLTable
    {

        [Column("payment")]
        public Moneys Payment { get; set; }

        [Column("payment_method_id")]
        public string PaymentMethod { get; set; }

        [Column("amazon_order_id")]
        public string AmazonOrderId { get; set; }
        public string? Amount { get; set; }

    }

}
