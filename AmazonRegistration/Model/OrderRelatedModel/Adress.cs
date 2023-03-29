using AlphaUtil.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using static AmazonSellerApi.Model.OrderRelatedModel.Address;

namespace AmazonSellerApi.Model.OrderRelatedModel
{
    [DataContract(Name = "address")]
    public partial class Address : SQLTable
    {
        public enum AddressTypeEnum
        {
            [EnumMember(Value = "Residential")]
            Residential = 1,

            [EnumMember(Value = "Commercial")]
            Commercial = 2

        }

        [Column("amazon_order_id")]
        public string AmazonOrderId { get; set; }
        [Column("state_or_region")]
        public string StateOrRegion { get; set; }

        [Column("postal_code")]
        public string PostalCode { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("address_type")]
        public AddressTypeEnum? AddressType { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("address_line1")]
        public string AddressLine1 { get; set; }
        [Column("address_line2")]

        public string AddressLine2 { get; set; }
        [Column("address_line3")]
        public string AddressLine3 { get; set; }
        [Column("country")]
        public string County { get; set; }
        [Column("district")]
        public string District { get; set; }
        [Column("municipality")]
        public string Municipality { get; set; }

        [Column("country_code")]
        public string CountryCode { get; set; }
        [Column("phone")]
        public string Phone { get; set; }


    }
    public class Address1
    {
        [Column("state_or_region")]
        public string StateOrRegion { get; set; }
        [Column("postal_code")]
        public string PostalCode { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("country_code")]
        public string CountryCode { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("address_line1")]
        public string AddressLine1 { get; set; }
        [Column("address_line2")]

        public string AddressLine2 { get; set; }
        [Column("address_line3")]
        public string AddressLine3 { get; set; }
        [Column("phone")]
        public string Phone { get; set; }


        [Column("country")]
        public string County { get; set; }
        [Column("district")]
        public string District { get; set; }
        [Column("municipality")]
        public string Municipality { get; set; }
        [Column("address_type")]
        public AddressTypeEnum? AddressType { get; set; }



    }
}
