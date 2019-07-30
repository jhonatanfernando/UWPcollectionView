using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SampleProject.Converter
{
    public class FlightClassImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            string result = string.Empty;

            switch ((string)value)
            {
                case "F":
                case "A":
                case "O":
                    result = Device.RuntimePlatform == Device.UWP ? "Images/icon_pax_class_first.png" : "icon_pax_class_first.png";
                    break;

                case "C":
                case "D":
                case "I":
                case "Z":
                case "J":
                case "P":
                case "R":
                    result = Device.RuntimePlatform == Device.UWP ? "Images/icon_pax_class_business.png" : "icon_pax_class_business.png";
                    break;

                default:
                    result = Device.RuntimePlatform == Device.UWP ? "Images/icon_pax_class_economy.png" : "icon_pax_class_economy.png";
                    break;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
