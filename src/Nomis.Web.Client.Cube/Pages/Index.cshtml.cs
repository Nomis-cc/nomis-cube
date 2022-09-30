using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nomis.Cubescan.Interfaces.Models;
using Nomis.Utils.Wrapper;
using Nomis.Web.Client.Cube.Managers;

namespace Nomis.Web.Client.Cube.Pages
{
    /// <summary>
    /// Index page.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ICubeManager _cubeManager;
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Wallet address.
        /// </summary>
        [BindProperty]
        public string? WalletAddress { get; set; }

        /// <summary>
        /// Blockchain.
        /// </summary>
        public string Blockchain => "Cube";

        /// <summary>
        /// Text box placeholder.
        /// </summary>
        public string Placeholder => "Cube wallet address";

        /// <summary>
        /// Cube wallet score result.
        /// </summary>
        public IResult<CubeWalletScore>? Result { get; private set; }

        /// <summary>
        /// Has no data.
        /// </summary>
        public bool HasNoData { get; private set; }

        /// <summary>
        /// Has error.
        /// </summary>
        public bool HasError { get; private set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string ErrorMessage { get; private set; } = "Error while calculating ...";

        /// <summary>
        /// Initialize <see cref="IndexModel"/>.
        /// </summary>
        /// <param name="cubeManager"><see cref="ICubeManager"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public IndexModel(
            ICubeManager cubeManager,
            ILogger<IndexModel> logger)
        {
            _cubeManager = cubeManager;
            _logger = logger;
        }

        /// <summary>
        /// On get method.
        /// </summary>
        public async Task<IActionResult> OnGetAsync(string address)
        {
            if (!string.IsNullOrWhiteSpace(address))
            {
                WalletAddress = address;

                try
                {
                    Result = await _cubeManager.GetWalletScoreAsync(address);
                    if (!Result.Succeeded)
                    {
                        HasError = true;
                        ErrorMessage = string.Join("<br>", Result.Messages);
                    }
                }
                catch (Exception e)
                {
                    HasError = true;
                    _logger.LogError(e, "Error calculating score for address = {Address}.", address);
                }
            }

            return Page();
        }

        /// <summary>
        /// On post method.
        /// </summary>
        public IActionResult OnPost()
        {
            return LocalRedirect($"~/?address={WalletAddress}");
        }
    }
}