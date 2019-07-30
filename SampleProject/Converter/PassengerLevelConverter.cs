using SampleProject.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SampleProject.Converter
{
    public class PassengerLevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is StatusLevel)) return Color.Transparent;

            var level = (StatusLevel)value;

            if (level == StatusLevel.Silver)
            {
                return Color.FromHex("#9e9991");
            }
            else if (level == StatusLevel.Platinum)
            {
                return Color.FromHex("#3f3b37");
            }
            else if (level == StatusLevel.Gold)
            {
                return Color.FromHex("#d8b831");
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
