using System.Windows;
using WarmupGen.Core;

namespace WarmupGen.Editor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly List<ExerciseOptionsModel> _options;

		public MainWindow()
		{
			InitializeComponent();

			_options = Generator.ExerciseLibrary.Exercises.Select(e => new ExerciseOptionsModel(e)).ToList();

			ExerciseList.ItemsSource = _options;

			SaveButton.Click += SaveButtonClicked;
		}

		private void SaveButtonClicked(object sender, RoutedEventArgs e)
		{
			foreach(var option in _options)
			{
				
			}	

			//Generator.WriteJson(Generator.ExerciseLibrary);
		}
	}
}