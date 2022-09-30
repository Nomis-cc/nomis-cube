using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nomis.Cubescan.Interfaces.Models
{
    /// <summary>
    /// Cubescan transfer list.
    /// </summary>
    /// <typeparam name="TListItem">Cubescan transfer.</typeparam>
    public interface ICubescanTransferList<TListItem>
        where TListItem : ICubescanTransfer
    {
        /// <summary>
        /// List of transfers.
        /// </summary>
        [JsonPropertyName("result")]
        [DataMember(EmitDefaultValue = true)]
        public List<TListItem> Data { get; set; }
    }
}