using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AmazonSellerApi.Model.OrderRelatedModel
{
    public class OrderData
    {

        public Payload? payload { get; set; }
    }

    public class Payload
    {
        public List<Order> Orders { get; set; }
        public DateTime LastUpdatedBefore { get; set; }
        public string? NextToken { get; set; }
    }
    public class Order
    {
        public enum OrderStatusEnum
        {
            [EnumMember(Value = "Pending")]
            Pending = 1,

            [EnumMember(Value = "Unshipped")]
            Unshipped = 2,

            [EnumMember(Value = "PartiallyShipped")]
            PartiallyShipped = 3,

            [EnumMember(Value = "Shipped")]
            Shipped = 4,

            [EnumMember(Value = "Canceled")]
            Canceled = 5,

            [EnumMember(Value = "Unfulfillable")]
            Unfulfillable = 6,

            [EnumMember(Value = "InvoiceUnconfirmed")]
            InvoiceUnconfirmed = 7,

            [EnumMember(Value = "PendingAvailability")]
            PendingAvailability = 8

        }
        public enum FulfillmentChannelEnum
        {
            [EnumMember(Value = "MFN")]
            MFN = 1,

            [EnumMember(Value = "AFN")]
            AFN = 2

        }
        public enum PaymentMethodEnum
        {
            [EnumMember(Value = "COD")]
            COD = 1,

            [EnumMember(Value = "CVS")]
            CVS = 2,

            [EnumMember(Value = "Other")]
            Other = 3

        }
        public enum OrderTypeEnum
        {
            [EnumMember(Value = "StandardOrder")]
            StandardOrder = 1,

            [EnumMember(Value = "LongLeadTimeOrder")]
            LongLeadTimeOrder = 2,

            [EnumMember(Value = "Preorder")]
            Preorder = 3,

            [EnumMember(Value = "BackOrder")]
            BackOrder = 4,

            [EnumMember(Value = "SourcingOnDemandOrder")]
            SourcingOnDemandOrder = 5

        }
        public enum BuyerInvoicePreferenceEnum
        {
            [EnumMember(Value = "INDIVIDUAL")]
            INDIVIDUAL = 1,

            [EnumMember(Value = "BUSINESS")]
            BUSINESS = 2

        }

        [Column("order_status")]
        public OrderStatusEnum? OrderStatus { get; set; }

        [Column("fulfillment_channel")]
        public FulfillmentChannelEnum? FulfillmentChannel { get; set; }


        [Column("payment_method")]
        public PaymentMethodEnum? PaymentMethod { get; set; }

        [Column("easy_ship_shipment_status")]
        public EasyShipShipmentStatus? EasyShipShipmentStatus { get; set; }

        [Column("order_type")]
        public OrderTypeEnum? OrderType { get; set; }


        [Column("buyer_invoice_preference")]
        public BuyerInvoicePreferenceEnum? BuyerInvoicePreference { get; set; }

        [Column("electronic_invoice_status")]
        public ElectronicInvoiceStatus? ElectronicInvoiceStatus { get; set; }
        [Column("amazon_order_id")]
        public string AmazonOrderId { get; set; }

        [Column("seller_order_id")]
        public string SellerOrderId { get; set; }

        [Column("purchase_date")]
        public string PurchaseDate { get; set; }

        [Column("last_update_date")]
        public string LastUpdateDate { get; set; }

        [Column("sales_channel")]
        public string SalesChannel { get; set; }

        [Column("order_channel")]
        public string OrderChannel { get; set; }

        [Column("ship_service_level")]
        public string ShipServiceLevel { get; set; }

        [Column("order_total")]

        public Moneys OrderTotal { get; set; }

        [Column("number_of_items_shipped")]
        public int NumberOfItemsShipped { get; set; }

        [Column("number_of_items_unshipped")]
        public int NumberOfItemsUnshipped { get; set; }

        [Column("payment_execution_detail")]
        [NotMapped]
        public List<PaymentExecutionDetailItem> PaymentExecutionDetail { get; set; }

        [Column("payment_method_details")]
        public List<string> PaymentMethodDetails { get; set; }

        [Column("marketplace_id")]
        public string MarketplaceId { get; set; }

        [Column("shipment_service_level_category")]
        public string ShipmentServiceLevelCategory { get; set; }

        [Column("cba_displayable_shipping_label")]
        public string CbaDisplayableShippingLabel { get; set; }

        [Column("earliest_ship_date")]
        public string EarliestShipDate { get; set; }

        [Column("latest_ship_date")]
        public string LatestShipDate { get; set; }

        [Column("earliest_delivery_date")]
        public string EarliestDeliveryDate { get; set; }

        [Column("latest_delivery_date")]
        public string LatestDeliveryDate { get; set; }

        [Column("is_business_order")]
        public bool? IsBusinessOrder { get; set; }

        [Column("is_prime")]
        public bool? IsPrime { get; set; }

        [Column("is_premium_order")]
        public bool? IsPremiumOrder { get; set; }

        [Column("is_global_express_enabled")]
        public bool? IsGlobalExpressEnabled { get; set; }

        [Column("replaced_order_id")]
        public string ReplacedOrderId { get; set; }

        [Column("is_replacement_order")]
        public bool? IsReplacementOrder { get; set; }

        [Column("promise_response_due_date")]
        public string PromiseResponseDueDate { get; set; }

        [Column("is_estimated_ship_date_set")]
        public bool? IsEstimatedShipDateSet { get; set; }

        [Column("is_sold_by_a_b")]
        public bool? IsSoldByAB { get; set; }

        [Column("is_i_b_a")]
        public bool? IsIBA { get; set; }

        [Column("default_ship_from_location_address")]
        public Address DefaultShipFromLocationAddress { get; set; }

        [Column("buyer_tax_information")]
        [NotMapped]
        public BuyerTaxInformation BuyerTaxInformation { get; set; }

        [Column("fulfillment_instruction")]
        [NotMapped]
        public FulfillmentInstruction FulfillmentInstruction { get; set; }
        [Column("is_i_s_p_u")]
        public bool? IsISPU { get; set; }

        [Column("is_access_point_order")]
        public bool? IsAccessPointOrder { get; set; }

        [Column("marketplace_tax_info")]
        [NotMapped]
        public MarketplaceTaxInfo MarketplaceTaxInfo { get; set; }

        [Column("seller_display_name")]
        public string SellerDisplayName { get; set; }
        [Column("shipping_address")]
        public Address1 ShippingAddress { get; set; }
        [Column("buyer_info")]
        [NotMapped]
        public BuyerInfo BuyerInfo { get; set; }

        [Column("automated_shipping_settings")]
        [NotMapped]
        public AutomatedShippingSettings AutomatedShippingSettings { get; set; }

        [Column("has_regulated_items")]
        public bool? HasRegulatedItems { get; set; }

        [Column("item_approval_types")]
        public List<ItemApprovalType> ItemApprovalTypes { get; set; }

        [Column("item_approval_status")]
        public List<ItemApprovalStatus> ItemApprovalStatus { get; set; }


    }
    public class inputFeild
    {
        public int sub_id { get; set; }
        public string? fromDate { get; set; }
        public string? toDate { get; set; }

    }
    public class DataTableOrder
    {
        public string amazon_order_id { get; set; }
        public string marchant_id { get; set; }
        public string seller_order_id { get; set; }
        public string purchase_date { get; set; }
        public string last_update_date { get; set; }
        public string sales_channel { get; set; }
        public string order_channel { get; set; }
        public string order_status { get; set; }
        public string easy_ship_shipment_status { get; set; }
        public string OrderType { get; set; }
        public string ship_service_level { get; set; }
        public string fulfillment_channel { get; set; }
        public int number_of_items_shipped { get; set; }
        public int number_of_items_unshipped { get; set; }
        public string payment_method { get; set; }
        public string marketplace_id { get; set; }
        public string shipment_service_level_category { get; set; }
        public string earliest_ship_date { get; set; }
        public string latest_ship_date { get; set; }
        public string earliest_delivery_date { get; set; }
        public string latest_delivery_date { get; set; }
        public string replaced_order_id { get; set; }
        public string promise_response_due_date { get; set; }
        public bool seller_display_name { get; set; }
        public bool is_prime { get; set; }
        public bool is_business_order { get; set; }
        public bool is_premium_order { get; set; }
        public bool is_global_express_enabled { get; set; }
        public bool is_replacement_order { get; set; }
        public bool is_estimated_ship_date_set { get; set; }
        public bool is_sold_by_a_b { get; set; }
        public bool is_i_b_a { get; set; }
        public bool is_i_s_p_u { get; set; }
        public bool is_access_point_order { get; set; }
        public bool has_regulated_items { get; set; }
    }



}

