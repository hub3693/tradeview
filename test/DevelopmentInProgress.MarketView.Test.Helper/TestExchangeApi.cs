﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevelopmentInProgress.MarketView.Interface.Events;
using DevelopmentInProgress.MarketView.Interface.Interfaces;
using DevelopmentInProgress.MarketView.Interface.Model;

namespace DevelopmentInProgress.MarketView.Test.Helper
{
    public class TestExchangeApi : IExchangeApi
    {
        public Task<string> CancelOrderAsync(User user, string symbol, long orderId, string newClientOrderId = null, long recWindow = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SymbolStats>> Get24HourStatisticsAsync(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<IEnumerable<SymbolStats>>();
            tcs.SetResult(MarketHelper.SymbolsStatistics);
            return tcs.Task;
        }

        public Task<AccountInfo> GetAccountInfoAsync(User user, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<AccountInfo>();
            tcs.SetResult(MarketHelper.AccountInfo);
            return tcs.Task;
        }

        public Task<IEnumerable<AggregateTrade>> GetAggregateTradesAsync(string symbol, int limit, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOpenOrdersAsync(User user, string symbol = null, long recWindow = 0, Action<Exception> exception = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<OrderBook> GetOrderBookAsync(string symbol, int limit, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Symbol>> GetSymbolsAsync(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<IEnumerable<Symbol>>();
            tcs.SetResult(MarketHelper.Symbols);
            return tcs.Task;
        }

        public Task<Order> PlaceOrder(User user, ClientOrder clientOrder, long recWindow = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void SubscribeAccountInfo(User user, Action<AccountInfoEventArgs> callback, Action<Exception> exception, CancellationToken cancellationToken)
        {
            // INTENTIONALLY EMPTY. LEAVE BLANK.
        }

        public void SubscribeAggregateTrades(string symbol, int limit, Action<AggregateTradeEventArgs> callback, Action<Exception> exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void SubscribeOrderBook(string symbol, int limit, Action<OrderBookEventArgs> callback, Action<Exception> exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void SubscribeStatistics(Action<StatiscticsEventArgs> callback, Action<Exception> exception, CancellationToken cancellationToken)
        {
            var ethStats = MarketHelper.EthStats;
            var symbolsStats = MarketHelper.SymbolsStatistics;

            var newStats = MarketHelper.EthStats_UpdatedLastPrice_Upwards;

            var updatedEthStats = symbolsStats.Single(s => s.Symbol.Equals("ETHBTC"));
            updatedEthStats.PriceChange = newStats.PriceChange;
            updatedEthStats.LastPrice = newStats.LastPrice;
            updatedEthStats.PriceChangePercent = newStats.PriceChangePercent;

            callback.Invoke(new StatiscticsEventArgs { Statistics = symbolsStats });
        }
    }
}
