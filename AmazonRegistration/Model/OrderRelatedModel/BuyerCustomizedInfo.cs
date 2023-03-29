using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace AmazonSellerApi.Model.OrderRelatedModel
{
    [DataContract(Name = "BuyerCustomizedInfoDetail")]
    public partial class BuyerCustomizedInfoDetail : IEquatable<BuyerCustomizedInfoDetail>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuyerCustomizedInfoDetail" /> class.
        /// </summary>
        /// <param name="customizedURL">The location of a zip file containing Amazon Custom data..</param>
        public BuyerCustomizedInfoDetail(string customizedURL = default)
        {
            CustomizedURL = customizedURL;
        }

        /// <summary>
        /// The location of a zip file containing Amazon Custom data.
        /// </summary>
        /// <value>The location of a zip file containing Amazon Custom data.</value>
        [DataMember(Name = "CustomizedURL", EmitDefaultValue = false)]
        public string CustomizedURL { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class BuyerCustomizedInfoDetail {\n");
            sb.Append("  CustomizedURL: ").Append(CustomizedURL).Append("\n");
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
            return Equals(input as BuyerCustomizedInfoDetail);
        }

        /// <summary>
        /// Returns true if BuyerCustomizedInfoDetail instances are equal
        /// </summary>
        /// <param name="input">Instance of BuyerCustomizedInfoDetail to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BuyerCustomizedInfoDetail input)
        {
            if (input == null)
            {
                return false;
            }
            return
                
                    CustomizedURL == input.CustomizedURL ||
                    CustomizedURL != null &&
                    CustomizedURL.Equals(input.CustomizedURL)
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
                if (CustomizedURL != null)
                {
                    hashCode = hashCode * 59 + CustomizedURL.GetHashCode();
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
