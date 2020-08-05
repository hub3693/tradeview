﻿//-----------------------------------------------------------------------
// <copyright file="TextToForegroundConverter.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DevelopmentInProgress.TradeView.Wpf.Controls.Converters
{
    /// <summary>
    /// Converts the value to a colour to set a texts foreground to.
    /// </summary>
    public class TextToForegroundConverter : IValueConverter
    {
        /// <summary>
        /// Converts the value to the converted type.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture information.</param>
        /// <returns>A converted type.</returns>
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            if (value == null
                || String.IsNullOrEmpty(value.ToString()))
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFBABABA"));
            }

            switch (value.ToString().ToUpperInvariant())
            {
                case "ERROR":
                    return new SolidColorBrush(Colors.Red);
                default:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFBABABA"));
            }
        }

        /// <summary>
        /// Converts the value back to the converted type.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture information.</param>
        /// <returns>A converted type.</returns>
        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}