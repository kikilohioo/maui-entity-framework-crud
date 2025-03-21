using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.Converters
{
    public class PriceForColorConverter : IValueConverter
    {
        // logica cuando se está enviando un dato del modelo data a la vista xaml
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var price = (decimal)value;

            if(price >= 0 && price <= 1000)
            {
                return Colors.Green;
            }

            return Colors.Red;
        }

        // logica cuando se está enviando un dato de la vista xaml al modelo data
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
