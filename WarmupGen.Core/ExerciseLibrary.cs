using System.Text.Json.Serialization;

namespace WarmupGen.Core
{
	public class ExerciseLibrary(List<Target> targets, List<Technique> techniques, List<Exercise> exercises)
	{
		public List<Target> Targets { get; } = targets;
		public List<Technique> Techniques { get; } = techniques;
		public List<Exercise> Exercises { get; } = exercises;

		static List<TechniqueExerciseMap>? _techniqueMaps;
		static List<TargetExerciseMap>? _targetMaps;

		[JsonIgnore]
		public List<TechniqueExerciseMap> TechniqueMaps => _techniqueMaps ??= BuildTechniqueMaps();

		[JsonIgnore]
		public List<TargetExerciseMap> TargetMaps => _targetMaps ??= BuildTargetMaps();

		List<TechniqueExerciseMap> BuildTechniqueMaps()
		{
			var maps = from technique in Techniques
					   select new TechniqueExerciseMap(technique, (
						   from exercise in Exercises
						   where exercise.Techniques.Contains(technique)
						   select exercise.Name
					   ).ToList());

			return maps.ToList();
		}

		List<TargetExerciseMap> BuildTargetMaps()
		{
			var maps = from target in Targets
					   select new TargetExerciseMap(target, (
						   from exercise in Exercises
						   where exercise.Targets.Contains(target)
						   select exercise.Name
					   ).ToList());

			return maps.ToList();
		}

		public Exercise ChooseRandomMatchingExercise(string? technique, string? target, IEnumerable<Exercise>? exclude)
		{
			// TODO keep a static empty list arround for this
			var candidates = Exercises
				.Except(exclude ?? new List<Exercise>())
				.Where(e => e.Matches(technique, target))
				.ToList();

			var count = candidates.Count;
			if (count == 0)
			{
				return Exercise.None;
			}

			var random = new Random(DateTime.Now.Millisecond);
			var index = random.Next(count - 1);

			return candidates[index];
		}
	}
	
	public record Target(int Id, string Name) : IReferenceable;
	public record Technique(int Id, string Name) : IReferenceable;
	
	public record TargetExerciseMap(Target Target, List<string> ExerciseNames);
	public record TechniqueExerciseMap(Technique Technique, List<string> ExerciseNames);
}
