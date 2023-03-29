using AlphaUtil.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace AmazonSellerApi.Model.OrderRelatedModel
{
    [DataContract(Name = "FulfillmentInstruction")]
    public partial class FulfillmentInstruction : SQLTable, IEquatable<FulfillmentInstruction>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FulfillmentInstruction" /> class.
        /// </summary>
        /// <param name="fulfillmentSupplySourceId">Denotes the recommended sourceId where the order should be fulfilled from..</param>
        public FulfillmentInstruction(string fulfillmentSupplySourceId = default)
        {
            FulfillmentSupplySourceId = fulfillmentSupplySourceId;
        }

        /// <summary>
        /// Denotes the recommended sourceId where the order should be fulfilled from.
        /// </summary>
        /// <value>Denotes the recommended sourceId where the order should be fulfilled from.</value>
        [DataMember(Name = "FulfillmentSupplySourceId", EmitDefaultValue = false)]
        public string FulfillmentSupplySourceId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class FulfillmentInstruction {\n");
            sb.Append("  FulfillmentSupplySourceId: ").Append(FulfillmentSupplySourceId).Append("\n");
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
            return Equals(input as FulfillmentInstruction);
        }

        /// <summary>
        /// Returns true if FulfillmentInstruction instances are equal
        /// </summary>
        /// <param name="input">Instance of FulfillmentInstruction to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FulfillmentInstruction input)
        {
            if (input == null)
            {
                return false;
            }
            return
                
                    FulfillmentSupplySourceId == input.FulfillmentSupplySourceId ||
                    FulfillmentSupplySourceId != null &&
                    FulfillmentSupplySourceId.Equals(input.FulfillmentSupplySourceId)
                ;
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
                if (FulfillmentSupplySourceId != null)
                {
                    hashCode = hashCode * 59 + FulfillmentSupplySourceId.GetHashCode();
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
