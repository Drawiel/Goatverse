using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Goatverse {
    public class ScreenSizeToFontSizeConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values.Length == 2 && values[0] is double width && values[1] is double height) {
                // Calcula el tamaño de fuente en función de las dimensiones de la ventana
                return Math.Max(12, Math.Min(width, height) / 100);
            }

            return 12; // Valor por defecto
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
