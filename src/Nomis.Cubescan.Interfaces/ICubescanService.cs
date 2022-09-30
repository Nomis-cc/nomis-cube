using Nomis.Cubescan.Interfaces.Models;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.Cubescan.Interfaces
{
    /// <summary>
    /// Cubescan service.
    /// </summary>
    public interface ICubescanService :
        IInfrastructureService
    {
        /// <summary>
        /// Client for interacting with Cubescan API.
        /// </summary>
        public ICubescanClient Client { get; }

        /// <summary>
        /// Get cube wallet stats by address.
        /// </summary>
        /// <param name="address">Cube wallet address.</param>
        /// <returns>Returns <see cref="CubeWalletScore"/> result.</returns>
        public Task<Result<CubeWalletScore>> GetWalletStatsAsync(string address);
    }
}