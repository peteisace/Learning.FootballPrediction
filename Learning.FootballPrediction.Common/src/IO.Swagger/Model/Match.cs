/* 
 * Match API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// Match
    /// </summary>
    [DataContract]
        public partial class Match :  IEquatable<Match>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Match" /> class.
        /// </summary>
        public Match()
        {
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int? Id { get; private set; }

        /// <summary>
        /// Gets or Sets Home
        /// </summary>
        [DataMember(Name="home", EmitDefaultValue=false)]
        public  Home { get; private set; }

        /// <summary>
        /// Gets or Sets Away
        /// </summary>
        [DataMember(Name="away", EmitDefaultValue=false)]
        public  Away { get; private set; }

        /// <summary>
        /// Gets or Sets Played
        /// </summary>
        [DataMember(Name="played", EmitDefaultValue=false)]
        public DateTime? Played { get; private set; }

        /// <summary>
        /// Gets or Sets MatchEvents
        /// </summary>
        [DataMember(Name="matchEvents", EmitDefaultValue=false)]
        public List<MatchEvent> MatchEvents { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Match {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Home: ").Append(Home).Append("\n");
            sb.Append("  Away: ").Append(Away).Append("\n");
            sb.Append("  Played: ").Append(Played).Append("\n");
            sb.Append("  MatchEvents: ").Append(MatchEvents).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as Match);
        }

        /// <summary>
        /// Returns true if Match instances are equal
        /// </summary>
        /// <param name="input">Instance of Match to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Match input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Home == input.Home ||
                    (this.Home != null &&
                    this.Home.Equals(input.Home))
                ) && 
                (
                    this.Away == input.Away ||
                    (this.Away != null &&
                    this.Away.Equals(input.Away))
                ) && 
                (
                    this.Played == input.Played ||
                    (this.Played != null &&
                    this.Played.Equals(input.Played))
                ) && 
                (
                    this.MatchEvents == input.MatchEvents ||
                    this.MatchEvents != null &&
                    input.MatchEvents != null &&
                    this.MatchEvents.SequenceEqual(input.MatchEvents)
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
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Home != null)
                    hashCode = hashCode * 59 + this.Home.GetHashCode();
                if (this.Away != null)
                    hashCode = hashCode * 59 + this.Away.GetHashCode();
                if (this.Played != null)
                    hashCode = hashCode * 59 + this.Played.GetHashCode();
                if (this.MatchEvents != null)
                    hashCode = hashCode * 59 + this.MatchEvents.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
