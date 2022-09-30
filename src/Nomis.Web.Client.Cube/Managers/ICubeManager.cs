using Nomis.Cubescan.Interfaces.Models;
using Nomis.Utils.Wrapper;
using Nomis.Web.Client.Common.Managers;

namespace Nomis.Web.Client.Cube.Managers
{
    /// <summary>
    /// Cube manager.
    /// </summary>
    public interface ICubeManager :
        IManager
    {
        /// <summary>
        /// Get cube wallet score.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        /// <returns>Returns result of <see cref="CubeWalletScore"/>.</returns>
        Task<IResult<CubeWalletScore>> GetWalletScoreAsync(string address);
    }
}