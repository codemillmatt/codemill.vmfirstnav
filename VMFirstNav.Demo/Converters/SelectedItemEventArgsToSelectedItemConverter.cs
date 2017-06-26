using System;
using System.Globalization;
using Xamarin.Forms;

namespace VMFirstNav.Demo
{
	/// <summary>
	/// Selected item event arguments to selected item converter.
	/// Thanks David Britch!
	/// https://github.com/davidbritch/xamarin-forms/tree/master/ItemSelectedBehavior
	/// </summary>
	public class SelectedItemEventArgsToSelectedItemConverter : IValueConverter
	{
		public SelectedItemEventArgsToSelectedItemConverter()
		{
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var eventArgs = value as SelectedItemChangedEventArgs;
			return eventArgs?.SelectedItem;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
