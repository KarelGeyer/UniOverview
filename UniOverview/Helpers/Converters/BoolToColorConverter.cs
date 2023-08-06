using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UniOverview.Helpers.Converters
{
	class BoolToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool boolValue)
			{
				if (parameter.Equals("IsCompleted"))
					return boolValue ? "#90EE90" : "#FFF";
				if (parameter.Equals("IsFailed"))
					return boolValue ? "#FF0000" : "#FFF";
			}

			return DependencyProperty.UnsetValue;
		}

		public object ConvertBack(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture
		)
		{
			throw new NotImplementedException();
		}
	}
}
