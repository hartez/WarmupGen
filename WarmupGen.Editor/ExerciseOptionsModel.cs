using WarmupGen.Core;

namespace WarmupGen.Editor
{
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
}