using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace UnoApp1.Presentation.Converters;

public class NullToActiveConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        bool inverse = parameter is string p && p.Equals("inverse", StringComparison.OrdinalIgnoreCase);

        return value != null ^ inverse;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

public class DateTimeFormatStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTime dateTime && parameter is string p)
        {
            return dateTime.ToString(p);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

public class TimeSpanToHumanFriendlyStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is TimeSpan timeSpan)
        {
            string result = "";

            if (timeSpan.Days > 0)
            {
                result += $"{timeSpan.Days} {(timeSpan.Days == 1 ? "day" : "days")} ";
            }

            if (timeSpan.Hours > 0)
            {
                result += $"{timeSpan.Hours} {(timeSpan.Hours == 1 ? "hour" : "hours")} ";
            }

            if (timeSpan.Minutes > 0)
            {
                result += $"{timeSpan.Minutes} {(timeSpan.Minutes == 1 ? "minute" : "minutes")} ";
            }

            if (timeSpan.Seconds > 0)
            {
                result += $"{timeSpan.Seconds} {(timeSpan.Seconds == 1 ? "second" : "seconds")} ";
            }

            return result.Trim();
        }

        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        bool inverse = parameter is string p && p.Equals("inverse", StringComparison.OrdinalIgnoreCase);

        return value != null ^ inverse ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
