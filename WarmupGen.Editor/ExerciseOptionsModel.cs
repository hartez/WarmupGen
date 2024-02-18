using WarmupGen.Core;

namespace WarmupGen.Editor
{
	public class ExerciseOptionsModel()
	{
		private readonly int _index;

		public ExerciseOptionsModel(Exercise exercise, int index) : this()
		{
			Exercise = exercise;
			_index = index;
			Targets = [];

			foreach (var target in Generator.ExerciseLibrary.Targets.OrderBy(t => t.Name))
			{
				Targets.Add(new TargetSelectedModel(target, exercise.Targets.Contains(target)));
			}

			Techniques = [];

			foreach (var technique in Generator.ExerciseLibrary.Techniques.OrderBy(t => t.Name))
			{
				Techniques.Add(new TechniqueSelectedModel(technique, exercise.Techniques.Contains(technique)));
			}
		}

		public Exercise Exercise { get; }

		public List<TargetSelectedModel> Targets { get; }
		public List<TechniqueSelectedModel> Techniques { get; }

		public int Index => _index;
	}
}