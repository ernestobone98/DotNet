using System.Globalization;
using MAUI.Reader.Model;

namespace MAUI.Reader.Pages;

public class ListGenresToConvert : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is List<Genre> genreList)
        {
            var stringList = genreList.Select(g => g.Name).ToList();
            return string.Join(", ", stringList);
        }
        if (value is List<Author> autList)
        {
            var stringList = autList.Select(g => g.Name).ToList();
            return string.Join(", ", stringList);
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return ((string)value).Split(",");
    }
}