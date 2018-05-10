﻿using DevelopmentInProgress.MarketView.Test.Helper;
using DevelopmentInProgress.Wpf.MarketView.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DevelopmentInProgress.Wpf.MarketView.Test
{
    [TestClass]
    public class SymbolExtensionsTest
    {
        [TestMethod]
        public void ConvertSymbols_Pass()
        {
            // Arrange
            var symbol = MarketHelper.Trx;

            // Act
            var viewSymbol = symbol.GetViewSymbol();
            var interfaceSymbol = viewSymbol.GetInterfaceSymbol();
            var missingOrderTypes = symbol.OrderTypes.Except(interfaceSymbol.OrderTypes).ToList();
            missingOrderTypes.AddRange(interfaceSymbol.OrderTypes.Except(symbol.OrderTypes));

            // Assert
            Assert.AreEqual(symbol.NotionalMinimumValue, interfaceSymbol.NotionalMinimumValue);
            Assert.AreEqual(symbol.BaseAsset.Symbol, interfaceSymbol.BaseAsset.Symbol);
            Assert.AreEqual(symbol.BaseAsset.Precision, interfaceSymbol.BaseAsset.Precision);
            Assert.AreEqual(symbol.QuoteAsset.Symbol, interfaceSymbol.QuoteAsset.Symbol);
            Assert.AreEqual(symbol.QuoteAsset.Precision, interfaceSymbol.QuoteAsset.Precision);
            Assert.AreEqual(symbol.Price.Minimum, interfaceSymbol.Price.Minimum);
            Assert.AreEqual(symbol.Price.Maximum, interfaceSymbol.Price.Maximum);
            Assert.AreEqual(symbol.Price.Increment, interfaceSymbol.Price.Increment);
            Assert.AreEqual(symbol.Quantity.Minimum, interfaceSymbol.Quantity.Minimum);
            Assert.AreEqual(symbol.Quantity.Maximum, interfaceSymbol.Quantity.Maximum);
            Assert.AreEqual(symbol.Quantity.Increment, interfaceSymbol.Quantity.Increment);
            Assert.AreEqual(symbol.Status, interfaceSymbol.Status);
            Assert.AreEqual(symbol.IsIcebergAllowed, interfaceSymbol.IsIcebergAllowed);
            Assert.IsFalse(missingOrderTypes.Any());
        }
    }
}
