using System.ComponentModel;
using WarmupGen.Core;

namespace WarmupGen.Editor
{
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

	public class TargetSelectedModel(Target option, bool selected) : SelectedModel<Target>(option, selected)
	{
		public string Name => SelectedOption.Name;
	}

	public class TechniqueSelectedModel(Technique option, bool selected) : SelectedModel<Technique>(option, selected)
	{
		public string Name => SelectedOption.Name;
	}
}