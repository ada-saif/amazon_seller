using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace AmazonSellerApi.Model.OrderRelatedModel
{

    [DataContract(Name = "ItemBuyerInfo")]
    public partial class ItemBuyerInfo : IEquatable<ItemBuyerInfo>, IValidatableObject
    {


        /// <summary>
        /// Gets or Sets BuyerCustomizedInfo
        /// </summary>
        [DataMember(Name = "BuyerCustomizedInfo", EmitDefaultValue = false)]
        public BuyerCustomizedInfoDetail BuyerCustomizedInfo { get; set; }

        /// <summary>
        /// Gets or Sets GiftWrapPrice
        /// </summary>
        [DataMember(Name = "GiftWrapPrice", EmitDefaultValue = false)]
        public Moneys GiftWrapPrice { get; set; }

        /// <summary>
        /// Gets or Sets GiftWrapTax
        /// </summary>
        [DataMember(Name = "GiftWrapTax", EmitDefaultValue = false)]
        public Moneys GiftWrapTax { get; set; }

        /// <summary>
        /// A gift message provided by the buyer.
        /// </summary>
        /// <value>A gift message provided by the buyer.</value>
        [DataMember(Name = "GiftMessageText", EmitDefaultValue = false)]
        public string GiftMessageText { get; set; }

        /// <summary>
        /// The gift wrap level specified by the buyer.
        /// </summary>
        /// <value>The gift wrap level specified by the buyer.</value>
        [DataMember(Name = "GiftWrapLevel", EmitDefaultValue = false)]
        public string GiftWrapLevel { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ItemBuyerInfo {\n");
            sb.Append("  BuyerCustomizedInfo: ").Append(BuyerCustomizedInfo).Append("\n");
            sb.Append("  GiftWrapPrice: ").Append(GiftWrapPrice).Append("\n");
            sb.Append("  GiftWrapTax: ").Append(GiftWrapTax).Append("\n");
            sb.Append("  GiftMessageText: ").Append(GiftMessageText).Append("\n");
            sb.Append("  GiftWrapLevel: ").Append(GiftWrapLevel).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return Equals(input as ItemBuyerInfo);
        }

        /// <summary>
        /// Returns true if ItemBuyerInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of ItemBuyerInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ItemBuyerInfo input)
        {
            if (input == null)
            {
                return false;
            }
            return
                (
                    BuyerCustomizedInfo == input.BuyerCustomizedInfo ||
                    BuyerCustomizedInfo != null &&
                    BuyerCustomizedInfo.Equals(input.BuyerCustomizedInfo)
                ) &&
                (
                    GiftWrapPrice == input.GiftWrapPrice ||
                    GiftWrapPrice != null &&
                    GiftWrapPrice.Equals(input.GiftWrapPrice)
                ) &&
                (
                    GiftWrapTax == input.GiftWrapTax ||
                    GiftWrapTax != null &&
                    GiftWrapTax.Equals(input.GiftWrapTax)
                ) &&
                (
                    GiftMessageText == input.GiftMessageText ||
                    GiftMessageText != null &&
                    GiftMessageText.Equals(input.GiftMessageText)
                ) &&
                (
                    GiftWrapLevel == input.GiftWrapLevel ||
                    GiftWrapLevel != null &&
                    GiftWrapLevel.Equals(input.GiftWrapLevel)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (BuyerCustomizedInfo != null)
                {
                    hashCode = hashCode * 59 + BuyerCustomizedInfo.GetHashCode();
                }
                if (GiftWrapPrice != null)
                {
                    hashCode = hashCode * 59 + GiftWrapPrice.GetHashCode();
                }
                if (GiftWrapTax != null)
                {
                    hashCode = hashCode * 59 + GiftWrapTax.GetHashCode();
                }
                if (GiftMessageText != null)
                {
                    hashCode = hashCode * 59 + GiftMessageText.GetHashCode();
                }
                if (GiftWrapLevel != null)
                {
                    hashCode = hashCode * 59 + GiftWrapLevel.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}

