using System.Net;

using Nethereum.Util;
using Nomis.Cubescan.Calculators;
using Nomis.Cubescan.Interfaces;
using Nomis.Cubescan.Interfaces.Models;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.Cubescan
{
    /// <inheritdoc cref="ICubescanService"/>
    internal sealed class CubescanService :
        ICubescanService,
        ITransientService
    {
        /// <summary>
        /// Initialize <see cref="CubescanService"/>.
        /// </summary>
        /// <param name="client"><see cref="ICubescanClient"/>.</param>
        public CubescanService(
            ICubescanClient client)
        {
            Client = client;
        }

        /// <inheritdoc/>
        public ICubescanClient Client { get; }

        /// <inheritdoc/>
        public async Task<Result<CubeWalletScore>> GetWalletStatsAsync(string address)
        {
            if (!new AddressUtil().IsValidAddressLength(address) || !new AddressUtil().IsValidEthereumAddressHexFormat(address))
            {
                throw new CustomException("Invalid address", statusCode: HttpStatusCode.BadRequest);
            }

            var balanceWei = (await Client.GetBalanceAsync(address)).Balance;
            var transactions = (await Client.GetTransactionsAsync<CubescanAccountNormalTransactions, CubescanAccountNormalTransaction>(address)).ToList();
            var internalTransactions = (await Client.GetTransactionsAsync<CubescanAccountInternalTransactions, CubescanAccountInternalTransaction>(address)).ToList();
            var erc20Tokens = (await Client.GetTransactionsAsync<CubescanAccountERC20TokenEvents, CubescanAccountERC20TokenEvent>(address)).ToList();
            var tokens = (await Client.GetTransactionsAsync<CubescanAccountERC721TokenEvents, CubescanAccountERC721TokenEvent>(address)).ToList();

            var walletStats = new CubeStatCalculator(
                    address,
                    decimal.TryParse(balanceWei, out var wei) ? wei : 0,
                    transactions,
                    internalTransactions,
                    tokens,
                    erc20Tokens)
                .GetStats();

            return await Result<CubeWalletScore>.SuccessAsync(new()
            {
                Stats = walletStats,
                Score = walletStats.GetScore()
            }, "Got cube wallet score.");
        }
    }
}