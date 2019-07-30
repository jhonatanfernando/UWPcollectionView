using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SampleProject.Converter
{
    public class PassengerVIPLevelStringColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is string)) return Color.FromHex("#00000000");
            var val = ((string)value).ToUpper();
            switch (val)
            {
                case "VIP":
                case "VIPC":
                    return Color.FromHex("#870000");
                case "VIPP":
                    return Color.FromHex("#A40D15");
                default:
                    return Color.FromHex("#00000000");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
