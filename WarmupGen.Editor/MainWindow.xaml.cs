using System.ComponentModel;
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

	public class ExerciseOptionsModel()
	{
		public ExerciseOptionsModel(Exercise exercise) : this()
		{
			Exercise = exercise;
			Targets = [];

			foreach (var target in Generator.ExerciseLibrary.Targets)
			{
				Targets.Add(new TargetSelectedModel(target, exercise.Targets.Contains(target)));
			}

			Techniques = [];

			foreach (var technique in Generator.ExerciseLibrary.Techniques)
			{
				Techniques.Add(new TechniqueSelectedModel(technique, exercise.Techniques.Contains(technique)));
			}
		}

		public Exercise Exercise { get; }

		public List<TargetSelectedModel> Targets { get; }
		public List<TechniqueSelectedModel> Techniques { get; }
	}

	public class TargetSelectedModel(Target option, bool selected) : SelectedModel<Target>(option, selected)
	{
		public string Name => SelectedOption.Name;
	}

	public class TechniqueSelectedModel(Technique option, bool selected) : SelectedModel<Technique>(option, selected)
	{
		public string Name => SelectedOption.Name;
	}

	public class SelectedModel<T> : INotifyPropertyChanged
	{
		private bool _selected;

		public SelectedModel(T option, bool selected)
		{
			SelectedOption = option;
			Selected = selected;
		}

		public T SelectedOption { get; }
		public bool Selected
		{
			get => _selected; set
			{
				_selected = value;
				OnPropertyChanged(nameof(Selected));
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}