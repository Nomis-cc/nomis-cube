using System.Numerics;

using Nomis.Cubescan.Extensions;
using Nomis.Cubescan.Interfaces.Models;
using Nomis.Utils.Extensions;

namespace Nomis.Cubescan.Calculators
{
    /// <summary>
    /// Cube wallet stats calculator.
    /// </summary>
    internal sealed class CubeStatCalculator
    {
        private readonly string _address;
        private readonly decimal _balance;
        private readonly IEnumerable<CubescanAccountNormalTransaction> _transactions;
        private readonly IEnumerable<CubescanAccountInternalTransaction> _internalTransactions;
        private readonly IEnumerable<ICubescanAccountNftTokenEvent> _tokenTransfers;
        private readonly IEnumerable<CubescanAccountERC20TokenEvent> _ecr20TokenTransfers;

        public CubeStatCalculator(
            string address,
            decimal balance,
            IEnumerable<CubescanAccountNormalTransaction> transactions,
            IEnumerable<CubescanAccountInternalTransaction> internalTransactions,
            IEnumerable<ICubescanAccountNftTokenEvent> tokenTransfers,
            IEnumerable<CubescanAccountERC20TokenEvent> ecr20TokenTransfers)
        {
            _address = address;
            _balance = balance;
            _transactions = transactions;
            _internalTransactions = internalTransactions;
            _tokenTransfers = tokenTransfers;
            _ecr20TokenTransfers = ecr20TokenTransfers;
        }

        private int GetWalletAge()
        {
            var firstTransaction = _transactions.First();
            return (int)((DateTime.UtcNow - firstTransaction.TimeStamp!.ToDateTime()).TotalDays / 30);
        }

        private IEnumerable<double> GetTransactionsIntervals()
        {
            var result = new List<double>();
            DateTime? lastDateTime = null;
            foreach (var transaction in _transactions.OrderByDescending(x => x.TimeStamp))
            {

                var transactionDate = transaction.TimeStamp!.ToDateTime();
                if (!lastDateTime.HasValue)
                {
                    lastDateTime = transactionDate;
                    continue;
                }

                var interval = Math.Abs((transactionDate - lastDateTime.Value).TotalHours);
                lastDateTime = transactionDate;
                result.Add(interval);
            }

            return result;
        }

        // TODO - check this!
        private BigInteger GetTokensSum(IEnumerable<ICubescanAccountNftTokenEvent> tokenList)
        {
            var transactions = tokenList.Select(x => x.Hash).ToHashSet();
            var result = new BigInteger();
            foreach (var st in _internalTransactions.Where(x => transactions.Contains(x.Hash)))
            {
                if (ulong.TryParse(st.Value, out var value))
                {
                    result += value;
                }
            }

            return result;
        }

        public CubeWalletStats GetStats()
        {
            if (!_transactions.Any())
            {
                return new()
                {
                    NoData = true
                };
            }

            var intervals = GetTransactionsIntervals().ToList();
            if (!intervals.Any())
            {
                return new()
                {
                    NoData = true
                };
            }

            var monthAgo = DateTime.Now.AddMonths(-1);

            var soldTokens = _tokenTransfers.Where(x => x.From?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true).ToList();
            var soldSum = GetTokensSum(soldTokens);

            var soldTokensIds = soldTokens.Select(x => x.GetTokenUid());
            var buyTokens = _tokenTransfers.Where(x => x.To?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true && soldTokensIds.Contains(x.GetTokenUid()));
            var buySum = GetTokensSum(buyTokens);

            var buyNotSoldTokens = _tokenTransfers.Where(x => x.To?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true && !soldTokensIds.Contains(x.GetTokenUid()));
            var buyNotSoldSum = GetTokensSum(buyNotSoldTokens);

            var holdingTokens = _tokenTransfers.Count() - soldTokens.Count;
            var nftWorth = buySum == 0 ? 0 : (decimal)soldSum / (decimal)buySum * (decimal)buyNotSoldSum;
            var contractsCreated = _transactions.Count(x => !string.IsNullOrWhiteSpace(x.ContractAddress));
            var totalTokens = _ecr20TokenTransfers.Select(x => x.TokenSymbol).Distinct();

            return new()
            {
                Balance = _balance.ToCube(),
                WalletAge = GetWalletAge(),
                TotalTransactions = _transactions.Count(),
                MinTransactionTime = intervals.Min(),
                MaxTransactionTime = intervals.Max(),
                AverageTransactionTime = intervals.Average(),
                WalletTurnover = _transactions.Sum(x =>
                {
                    if (ulong.TryParse(x.Value, out var value))
                    {
                        return (decimal)value;
                    }

                    return (decimal)0;
                }).ToCube(),
                LastMonthTransactions = _transactions.Count(x => x.TimeStamp!.ToDateTime() > monthAgo),
                TimeFromLastTransaction = (int)((DateTime.UtcNow - _transactions.Last().TimeStamp!.ToDateTime()).TotalDays / 30),
                NftHolding = holdingTokens,
                NftTrading = (soldSum - buySum).ToCube(),
                NftWorth = nftWorth.ToCube(),
                DeployedContracts = contractsCreated,
                TokensHolding = totalTokens.Count()
            };
        }
    }
}