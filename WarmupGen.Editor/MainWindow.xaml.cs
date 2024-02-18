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

			_options = Generator.ExerciseLibrary.Exercises
				.OrderBy(exercise => exercise.Name)
				.Select((exercise, index) => new ExerciseOptionsModel(exercise, index))
				.ToList();

			ExerciseList.ItemsSource = _options;

			SaveButton.Click += SaveButtonClicked;
		}

		private void SaveButtonClicked(object sender, RoutedEventArgs e)
		{
			foreach (var option in _options)
			{
				// Find the corresponding exercise in the library

				var exercise = Generator.ExerciseLibrary.Exercises.FirstOrDefault(e => e == option.Exercise)
					?? throw new InvalidOperationException($"Exercise {option.Exercise.Name} not found");

				// Clear out the current targets and replace them with the selected ones
				exercise.Targets.Clear();
				foreach (var selection in option.Targets.Where(t => t.Selected))
				{
					exercise.Targets.Add(selection.SelectedOption);
				}

				// Clear out the current techniques and replace them with the selected ones
				exercise.Techniques.Clear();
				foreach (var selection in option.Techniques.Where(t => t.Selected))
				{
					exercise.Techniques.Add(selection.SelectedOption);
				}
			}

			Generator.WriteJson(Generator.ExerciseLibrary, @"..\..\..\..\WarmupGen.Core\");

			MessageBox.Show("Library updated!", "Success", MessageBoxButton.OK);
		}
	}
}