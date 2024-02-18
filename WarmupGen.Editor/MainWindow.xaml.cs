using System.Windows;
using WarmupGen.Core;

namespace WarmupGen.Editor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			var exerciseOptions = Generator.ExerciseLibrary.Exercises.Select(e => new ExerciseOptionsModel(e)).ToList();

			ExerciseList.ItemsSource = exerciseOptions;
		}
	}
}