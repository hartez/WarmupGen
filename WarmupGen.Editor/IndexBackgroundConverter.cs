using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WarmupGen.Editor
{
	public class IndexBackgroundConverter : IValueConverter
	{
		private Color _color;
		private Color _alternateColor;

		private SolidColorBrush? _mainBrush;
		private SolidColorBrush? _alternateBrush;

		public Color Color
		{
			get => _color; 
			set
			{
				_color = value;
				_mainBrush = new SolidColorBrush(_color);
			}
		}

		public Color AlternateColor
		{
			get => _alternateColor; 
			set
			{
				_alternateColor = value;
				_alternateBrush = new SolidColorBrush(_alternateColor);
			}
		}

		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is not int index)
			{
				throw new ArgumentException("Index value must be an integer");
			}

			return (index % 2 == 0) ? _alternateBrush : _mainBrush;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}