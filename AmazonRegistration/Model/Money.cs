using AlphaUtil.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AmazonSellerApi.Model
{
    

        public class Moneys : SQLTable
        {
            [DataMember]
            [Column("amazon_order_id")]
            public string AmazonOrderId { get; set; }
            [DataMember]

            [Column("currency_code")]
            public string CurrencyCode { get; set; }
            [DataMember]

            [Column("amount")]
            public decimal Amount { get; set; }
        

    }
}
