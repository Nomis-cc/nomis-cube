using System.Text.Json.Serialization;

namespace Nomis.Cubescan.Interfaces.Models
{
    /// <summary>
    /// Cubescan account.
    /// </summary>
    public class CubescanAccount
    {
        /// <summary>
        /// Status.
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Balance.
        /// </summary>
        [JsonPropertyName("result")]
        public string? Balance { get; set; }
    }
}