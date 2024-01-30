namespace WarmupGen.Core
{
	public class Segment
	{
		private string? _target;
		private string? _technique;

		public string? Technique
		{
			get => _technique;
			set
			{
				if (_technique != value)
				{
					_technique = value;
					OnCriteriaChanged();
				}
			}
		}

		public string? Target
		{
			get => _target;
			set
			{
				if (_target != value)
				{
					_target = value;
					OnCriteriaChanged();
				}
			}
		}

		public event EventHandler? CriteriaChanged;

		public Exercise Exercise { get; set; } = Exercise.None;

		public Segment() : this(null, null) { }

		public Segment(string? technique, string? category)
		{
			Technique = technique;
			Target = category;
		}

		void OnCriteriaChanged()
		{
			CriteriaChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}