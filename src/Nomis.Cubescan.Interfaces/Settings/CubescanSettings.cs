using Nomis.Utils.Contracts.Common;

namespace Nomis.Cubescan.Interfaces.Settings
{
    /// <summary>
    /// Cubescan settings.
    /// </summary>
    public class CubescanSettings :
        ISettings
    {
        /// <summary>
        /// API key for cubescan.
        /// </summary>
        /// <remarks>
        /// <see href="https://www.cubescan.network/en-us/apis"/>
        /// </remarks>
        public string? ApiKey { get; set; }

        /// <summary>
        /// API base URL.
        /// </summary>
        /// <remarks>
        /// <see href="https://www.cubescan.network/en-us/apis"/>
        /// </remarks>
        public string? ApiBaseUrl { get; set; }
    }
}