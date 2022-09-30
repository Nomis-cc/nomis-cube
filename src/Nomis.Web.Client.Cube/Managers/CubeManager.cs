using Microsoft.Extensions.Options;
using Nomis.Cubescan.Interfaces.Models;
using Nomis.Utils.Wrapper;
using Nomis.Web.Client.Common.Extensions;
using Nomis.Web.Client.Common.Settings;
using Nomis.Web.Client.Cube.Routes;

namespace Nomis.Web.Client.Cube.Managers
{
    /// <inheritdoc cref="ICubeManager" />
    public class CubeManager :
        ICubeManager
    {
        private readonly HttpClient _httpClient;
        private readonly CubeEndpoints _endpoints;

        /// <summary>
        /// Initialize <see cref="CubeManager"/>.
        /// </summary>
        /// <param name="webApiSettings"><see cref="WebApiSettings"/>.</param>
        public CubeManager(
            IOptions<WebApiSettings> webApiSettings)
        {
            _httpClient = new()
            {
                BaseAddress = new(webApiSettings.Value?.ApiBaseUrl ?? throw new ArgumentNullException(nameof(webApiSettings.Value.ApiBaseUrl)))
            };
            _endpoints = new(webApiSettings.Value?.ApiBaseUrl ?? throw new ArgumentNullException(nameof(webApiSettings.Value.ApiBaseUrl)));
        }

        /// <inheritdoc />
        public async Task<IResult<CubeWalletScore>> GetWalletScoreAsync(string address)
        {
            var response = await _httpClient.GetAsync(_endpoints.GetWalletScore(address));
            return await response.ToResultAsync<CubeWalletScore>();
        }
    }
}