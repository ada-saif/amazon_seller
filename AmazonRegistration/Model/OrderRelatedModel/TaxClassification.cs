using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace AmazonSellerApi.Model.OrderRelatedModel
{
    [DataContract(Name = "TaxClassification")]
    public partial class TaxClassification : IEquatable<TaxClassification>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxClassification" /> class.
        /// </summary>
        /// <param name="name">The type of tax..</param>
        /// <param name="value">The buyer&#39;s tax identifier..</param>
        public TaxClassification(string name = default, string value = default)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// The type of tax.
        /// </summary>
        /// <value>The type of tax.</value>
        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The buyer&#39;s tax identifier.
        /// </summary>
        /// <value>The buyer&#39;s tax identifier.</value>
        [DataMember(Name = "Value", EmitDefaultValue = false)]
        public string Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class TaxClassification {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
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
            return Equals(input as TaxClassification);
        }

        /// <summary>
        /// Returns true if TaxClassification instances are equal
        /// </summary>
        /// <param name="input">Instance of TaxClassification to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TaxClassification input)
        {
            if (input == null)
            {
                return false;
            }
            return
                (
                    Name == input.Name ||
                    Name != null &&
                    Name.Equals(input.Name)
                ) &&
                (
                    Value == input.Value ||
                    Value != null &&
                    Value.Equals(input.Value)
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
                if (Name != null)
                {
                    hashCode = hashCode * 59 + Name.GetHashCode();
                }
                if (Value != null)
                {
                    hashCode = hashCode * 59 + Value.GetHashCode();
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
