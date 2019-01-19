﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace XamlExtensions.Markup
{
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public partial class ComposeExtension : MarkupExtension, IValueConverter
    {
        [ConstructorArgument(nameof(Converters))]
        public ConverterCollection Converters { get; set; } = new ConverterCollection();

        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Converters.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            Converters.Aggregate(value, (current, converter) => converter.ConvertBack(current, targetType, parameter, culture));
    }

    public class ConverterCollection : Collection<IValueConverter>
    {
    }
}