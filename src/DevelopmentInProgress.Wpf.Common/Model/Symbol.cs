﻿using DevelopmentInProgress.MarketView.Interface.Extensions;
using Interface = DevelopmentInProgress.MarketView.Interface.Model;
using System.Collections.Generic;

namespace DevelopmentInProgress.Wpf.Common.Model
{
    public class Symbol : EntityBase
    {
        private int lastPriceChangeDirection;
        private int priceChangePercentDirection;
        private bool isFavourite;

        public string ExchangeSymbol { get; set; }
        public decimal NotionalMinimumValue { get; set; }
        public Interface.Asset BaseAsset { get; set; }
        public Interface.InclusiveRange Price { get; set; }
        public Interface.InclusiveRange Quantity { get; set; }
        public Interface.Asset QuoteAsset { get; set; }
        public Interface.SymbolStatus Status { get; set; }
        public bool IsIcebergAllowed { get; set; }
        public IEnumerable<Interface.OrderType> OrderTypes { get; set; }
        public SymbolStatistics SymbolStatistics { get; set; }

        public string Name { get { return $"{BaseAsset.Symbol}{QuoteAsset.Symbol}"; } }

        public bool IsFavourite 
        {
            get { return isFavourite; }
            set
            {
                if(isFavourite != value)
                {
                    isFavourite = value;
                    OnPropertyChanged("IsFavourite");
                }
            }
        }

        public int LastPriceChangeDirection
        {
            get { return lastPriceChangeDirection; }
            set
            {
                if (lastPriceChangeDirection != value)
                {
                    lastPriceChangeDirection = value;
                    OnPropertyChanged("LastPriceChangeDirection");
                }
            }
        }

        public int PriceChangePercentDirection
        {
            get { return priceChangePercentDirection; }
            set
            {
                if (priceChangePercentDirection != value)
                {
                    priceChangePercentDirection = value;
                    OnPropertyChanged("PriceChangePercentDirection");
                }
            }
        }

        public int QuantityPrecision
        {
            get
            {
                return Quantity.Increment.GetPrecision();
            }
        }

        public int PricePrecision
        {
            get
            {
                return Price.Increment.GetPrecision();
            }
        }

        public override string ToString()
        {
            return $"{BaseAsset.Symbol} / {QuoteAsset.Symbol}";
        }
    }
}
