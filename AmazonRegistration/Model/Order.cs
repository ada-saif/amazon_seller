using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AmazonRegistration.Model
{
    public class Order
    {
        [Key]
        public string amazon_order_id { get; set; }
        public string marchant_id { get; set; }
        public string seller_order_id { get; set; }
        public string purchase_date { get; set; }
        public string last_update_date { get; set; }
        public string order_status { get; set; }
        public string fulfillment_channel { get; set; }
        public string sales_channel { get; set; }
        public string OrderChannel { get; set; }
        public string ship_service_level { get; set; }
        public int number_of_items_shipped { get; set; }
        public int number_of_items_unshipped { get; set; }
        public string payment_method { get; set; }
        public string marketplace_id { get; set; }
        public string shipment_service_level_category { get; set; }
        public string easy_ship_shipment_status { get; set; }
        public string OrderType { get; set; }
        public string earliest_ship_date { get; set; }
        public string latest_ship_date { get; set; }
        public string earliest_delivery_date { get; set; }
        public string latest_delivery_date { get; set; }
        public bool is_business_order { get; set; }
        public bool is_prime { get; set; }
        public bool is_premium_order { get; set; }
        public bool is_global_express_enabled { get; set; }
        public bool is_replacement_order { get; set; }
        public string PromiseResponseDueDate { get; set; }
        public bool is_estimated_ship_date_set { get; set; }
        public bool is_sold_by_a_b { get; set; }
        public bool is_i_b_a { get; set; }
        public bool is_i_s_p_u { get; set; }
        public bool is_access_point_order { get; set; }
        public bool has_regulated_items { get; set; }
    }
    public enum EasyShipShipmentStatus
    {
        /// <summary>
        /// Enum PendingSchedule for value: PendingSchedule
        /// </summary>
        [EnumMember(Value = "PendingSchedule")]
        PendingSchedule = 1,

        /// <summary>
        /// Enum PendingPickUp for value: PendingPickUp
        /// </summary>
        [EnumMember(Value = "PendingPickUp")]
        PendingPickUp = 2,

        /// <summary>
        /// Enum PendingDropOff for value: PendingDropOff
        /// </summary>
        [EnumMember(Value = "PendingDropOff")]
        PendingDropOff = 3,

        /// <summary>
        /// Enum LabelCanceled for value: LabelCanceled
        /// </summary>
        [EnumMember(Value = "LabelCanceled")]
        LabelCanceled = 4,

        /// <summary>
        /// Enum PickedUp for value: PickedUp
        /// </summary>
        [EnumMember(Value = "PickedUp")]
        PickedUp = 5,

        /// <summary>
        /// Enum DroppedOff for value: DroppedOff
        /// </summary>
        [EnumMember(Value = "DroppedOff")]
        DroppedOff = 6,

        /// <summary>
        /// Enum AtOriginFC for value: AtOriginFC
        /// </summary>
        [EnumMember(Value = "AtOriginFC")]
        AtOriginFC = 7,

        /// <summary>
        /// Enum AtDestinationFC for value: AtDestinationFC
        /// </summary>
        [EnumMember(Value = "AtDestinationFC")]
        AtDestinationFC = 8,

        /// <summary>
        /// Enum Delivered for value: Delivered
        /// </summary>
        [EnumMember(Value = "Delivered")]
        Delivered = 9,

        /// <summary>
        /// Enum RejectedByBuyer for value: RejectedByBuyer
        /// </summary>
        [EnumMember(Value = "RejectedByBuyer")]
        RejectedByBuyer = 10,

        /// <summary>
        /// Enum Undeliverable for value: Undeliverable
        /// </summary>
        [EnumMember(Value = "Undeliverable")]
        Undeliverable = 11,

        /// <summary>
        /// Enum ReturningToSeller for value: ReturningToSeller
        /// </summary>
        [EnumMember(Value = "ReturningToSeller")]
        ReturningToSeller = 12,

        /// <summary>
        /// Enum ReturnedToSeller for value: ReturnedToSeller
        /// </summary>
        [EnumMember(Value = "ReturnedToSeller")]
        ReturnedToSeller = 13,

        /// <summary>
        /// Enum Lost for value: Lost
        /// </summary>
        [EnumMember(Value = "Lost")]
        Lost = 14,

        /// <summary>
        /// Enum OutForDelivery for value: OutForDelivery
        /// </summary>
        [EnumMember(Value = "OutForDelivery")]
        OutForDelivery = 15,

        /// <summary>
        /// Enum Damaged for value: Damaged
        /// </summary>
        [EnumMember(Value = "Damaged")]
        Damaged = 16

    }
    public enum OrderElectronicInvoiceStatusEnum
    {
        /// <summary>
        /// Enum NotRequired for value: NotRequired
        /// </summary>
        [EnumMember(Value = "NotRequired")]
        NotRequired = 1,

        /// <summary>
        /// Enum NotFound for value: NotFound
        /// </summary>
        [EnumMember(Value = "NotFound")]
        NotFound = 2,

        /// <summary>
        /// Enum Processing for value: Processing
        /// </summary>
        [EnumMember(Value = "Processing")]
        Processing = 3,

        /// <summary>
        /// Enum Errored for value: Errored
        /// </summary>
        [EnumMember(Value = "Errored")]
        Errored = 4,

        /// <summary>
        /// Enum Accepted for value: Accepted
        /// </summary>
        [EnumMember(Value = "Accepted")]
        Accepted = 5

    }

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


    public class OrderListRequest
	{
		public int? Start { get; set; }
		public int? Length { get; set; }
		public string Draw { get; set; }
		public string SortColumn { get; set; }
		public string SortColumnDirection { get; set; }
		public string SearchValue { get; set; }
    }
    public class inputFeild 
    {
        public int  p_id { get; set; }
        public string  fromDate { get; set; }
        public string  toDate { get; set; }
    }

}
