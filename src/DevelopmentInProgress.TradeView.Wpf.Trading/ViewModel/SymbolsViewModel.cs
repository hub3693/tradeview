﻿using DevelopmentInProgress.Common.Extensions;
using DevelopmentInProgress.TradeView.Wpf.Common.Model;
using DevelopmentInProgress.TradeView.Wpf.Trading.Events;
using DevelopmentInProgress.TradeView.Wpf.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using DevelopmentInProgress.TradeView.Wpf.Common.Cache;
using System.Threading.Tasks;
using DevelopmentInProgress.TradeView.Wpf.Common.ViewModel;
using Prism.Logging;
using System.Diagnostics;

namespace DevelopmentInProgress.TradeView.Wpf.Trading.ViewModel
{
    public class SymbolsViewModel : ExchangeViewModel
    {
        private ISymbolsCacheFactory symbolsCacheFactory;
        private ISymbolsCache symbolsCache;
        private List<Symbol> symbols;
        private Symbol selectedSymbol;
        private UserAccount accountPreferences;
        private bool isLoadingSymbols;
        private bool disposed;

        public SymbolsViewModel(IWpfExchangeService exchangeService, ISymbolsCacheFactory symbolsCacheFactory, ILoggerFacade logger)
            : base(exchangeService, logger)
        {
            this.symbolsCacheFactory = symbolsCacheFactory;
        }

        public event EventHandler<SymbolsEventArgs> OnSymbolsNotification;

        public List<Symbol> Symbols
        {
            get { return symbols; }
            set
            {
                if (symbols != value)
                {
                    symbols = value;
                    if(symbols != null)
                    {
                        OnLoadedSymbols(symbols);
                    }

                    OnPropertyChanged("Symbols");
                }
            }
        }

        public Symbol SelectedSymbol
        {
            get { return selectedSymbol; }
            set
            {
                if (selectedSymbol != value)
                {
                    selectedSymbol = value;
                    OnSelectedSymbol(selectedSymbol);
                    OnPropertyChanged("SelectedSymbol");
                }
            }
        }

        public UserAccount AccountPreferences
        {
            get { return accountPreferences; }
            set
            {
                if (accountPreferences != value)
                {
                    accountPreferences = value;
                }
            }
        }

        public bool IsLoadingSymbols
        {
            get { return isLoadingSymbols; }
            set
            {
                if (isLoadingSymbols != value)
                {
                    isLoadingSymbols = value;
                    OnPropertyChanged("IsLoadingSymbols");
                }
            }
        }

        public override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                symbolsCache.OnSymbolsCacheException -= SymbolsCacheException;
            }

            disposed = true;
        }

        public void SetAccount(UserAccount userAccount)
        {
            try
            {
                IsLoadingSymbols = true;

                AccountPreferences = userAccount;

                GetSymbols().FireAndForget();
            }
            catch (Exception ex)
            {
                OnException("SymbolsViewModel.SetUser", ex);
            }
        }

        private async Task GetSymbols()
        {
            try
            {
                var sw = new Stopwatch();
                sw.Start();
                Logger.Log($"1 - {sw.Elapsed}", Category.Info, Priority.High);

                if (symbolsCache == null)
                {
                    symbolsCache = symbolsCacheFactory.GetSymbolsCache(AccountPreferences.Exchange);
                    symbolsCache.OnSymbolsCacheException += SymbolsCacheException;
                }

                Logger.Log($"2 - {sw.Elapsed}", Category.Info, Priority.High);

                var results = await symbolsCache.GetSymbols(AccountPreferences.Preferences.FavouriteSymbols);

                Logger.Log($"3 - {sw.Elapsed}", Category.Info, Priority.High);

                Symbols = new List<Symbol>(results);

                Logger.Log($"4 - {sw.Elapsed}", Category.Info, Priority.High);

                SetPreferences();

                Logger.Log($"5 - {sw.Elapsed}", Category.Info, Priority.High);
            }
            catch(Exception ex)
            {
                OnException("SymbolsViewModel.GetSymbols", ex);
            }
            finally
            {
                IsLoadingSymbols = false;
            }
        }

        private void SymbolsCacheException(object sender, Exception exception)
        {
            OnException("SymbolsViewModel.GetSymbols - ExchangeService.GetSymbolsSubscription", exception);
        }

        private void OnException(string message, Exception exception)
        {
            var onSymbolsNotification = OnSymbolsNotification;
            onSymbolsNotification?.Invoke(this, new SymbolsEventArgs { Message = message, Exception = exception });
        }

        private void OnSelectedSymbol(Symbol symbol)
        {
            var onSymbolsNotification = OnSymbolsNotification;
            onSymbolsNotification?.Invoke(this, new SymbolsEventArgs { Value = symbol });
        }

        private void OnLoadedSymbols(List<Symbol> symbols)
        {
            var onSymbolsNotification = OnSymbolsNotification;
            onSymbolsNotification?.Invoke(this, new SymbolsEventArgs { Symbols = symbols });
        }

        private void SetPreferences()
        {
            if (AccountPreferences != null 
                && AccountPreferences.Preferences != null 
                && Symbols != null 
                && Symbols.Any())
            {
                if (AccountPreferences.Preferences.FavouriteSymbols != null
                    && AccountPreferences.Preferences.FavouriteSymbols.Any())
                {
                    if (!string.IsNullOrWhiteSpace(AccountPreferences.Preferences.SelectedSymbol))
                    {
                        var symbol = Symbols.FirstOrDefault(s => s.Name.Equals(AccountPreferences.Preferences.SelectedSymbol));
                        if (symbol != null)
                        {
                            SelectedSymbol = symbol;
                        }
                    }
                }
            }
        }
    }
}
