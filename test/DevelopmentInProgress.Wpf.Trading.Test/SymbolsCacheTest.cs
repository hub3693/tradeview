﻿using DevelopmentInProgress.MarketView.Test.Helper;
using DevelopmentInProgress.Wpf.Common.Model;
using DevelopmentInProgress.Wpf.Common.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Interface = DevelopmentInProgress.MarketView.Interface.Model;

namespace DevelopmentInProgress.Wpf.Trading.Test
{
    [TestClass]
    public class SymbolsCacheTest
    {
        [TestMethod]
        public async Task GetSingleBalanceUDTValue_BTC()
        {
            // Arrange
            var exchangeApi = ExchangeServiceHelper.GetExchangeService(ExchangeServiceType.Standard);
            var exchangeService = new WpfExchangeService(exchangeApi);
            var symbolsCache = SymbolsCacheHelper.GetSymbolsCache(exchangeService);
            var symbols = await symbolsCache.GetSymbols(new[] { "BIBBTC" });

            var balances = new List<Interface.AccountBalance>();
            balances.Add(new Interface.AccountBalance { Asset = "BTC", Free = 0.00794722m });

            var accountInfo = new Interface.AccountInfo
            {
                Balances = balances
            };

            var account = new Account(accountInfo);

            // Act
            symbolsCache.ValueAccount(account);

            // Assert
            Assert.AreEqual(account.USDTValue, 75.14m);
            Assert.AreEqual(account.BTCValue, 0.00794722m);
        }

        [TestMethod]
        public async Task GetSingleBalanceUDTValue_BNB()
        {
            // Arrange
            var exchangeApi = ExchangeServiceHelper.GetExchangeService(ExchangeServiceType.Standard);
            var exchangeService = new WpfExchangeService(exchangeApi);
            var symbolsCache = SymbolsCacheHelper.GetSymbolsCache(exchangeService);
            var symbols = await symbolsCache.GetSymbols(new[] { "BNBBTC" });

            var balances = new List<Interface.AccountBalance>();
            balances.Add(new Interface.AccountBalance { Asset = "BNB", Free = 1.88373641m });

            var accountInfo = new Interface.AccountInfo
            {
                Balances = balances
            };

            var account = new Account(accountInfo);

            // Act
            symbolsCache.ValueAccount(account);

            // Assert
            Assert.AreEqual(account.USDTValue, 25.93m);
            Assert.AreEqual(account.BTCValue, 0.00274272m);
        }

        [TestMethod]
        public async Task GetMultipleBalancesUDTValue_BTC_BNB()
        {
            // Arrange
            var exchangeApi = ExchangeServiceHelper.GetExchangeService(ExchangeServiceType.Standard);
            var exchangeService = new WpfExchangeService(exchangeApi);
            var symbolsCache = SymbolsCacheHelper.GetSymbolsCache(exchangeService);
            var symbols = await symbolsCache.GetSymbols(new[] { "BNBBTC" });

            var balances = new List<Interface.AccountBalance>();
            balances.Add(new Interface.AccountBalance { Asset = "BTC", Free = 0.00396715m });
            balances.Add(new Interface.AccountBalance { Asset = "BNB", Free = 0.94444141m });

            var accountInfo = new Interface.AccountInfo
            {
                Balances = balances
            };

            var account = new Account(accountInfo);

            // Act
            symbolsCache.ValueAccount(account);
            
            // Assert
            Assert.AreEqual(account.USDTValue, 50.51m);
            Assert.AreEqual(account.BTCValue, 0.00534226m);
        }
    }
}
