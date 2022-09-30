using System.Text.Json.Serialization;

namespace Nomis.Cubescan.Interfaces.Models
{
    /// <summary>
    /// Cubescan transfer.
    /// </summary>
    public interface ICubescanTransfer
    {
        /// <summary>
        /// Block number.
        /// </summary>
        [JsonPropertyName("blockNumber")]
        public string? BlockNumber { get; set; }
    }
}