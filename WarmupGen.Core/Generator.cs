using System.Reflection;
using System.Text.Json;

namespace WarmupGen.Core
{
	public class Generator
	{
		public static ExerciseLibrary ExerciseLibrary => _exerciseLibrary ??= LoadLibrary();

		static ExerciseLibrary? _exerciseLibrary;

		public Generator()
		{
		}

		public static Warmup GenerateWarmup(int segmentCount)
		{
			var warmup = new Warmup();

			for (int n = 0; n < segmentCount; n++)
			{
				warmup.Add(new Segment(null, null));
			}

			return warmup;
		}

		static ExerciseLibrary LoadLibrary()
		{
			var targets = LoadTargets();
			var techniques = LoadTechniques();
			var exercises = LoadExercises(targets, techniques);

			return new ExerciseLibrary(targets, techniques, exercises);
		}

		static List<Exercise> LoadExercises(List<Target> targets, List<Technique> techniques)
		{
			JsonSerializerOptions exerciseOptions = new();
			exerciseOptions.Converters.Add(new TechniqueConverter(techniques));
			exerciseOptions.Converters.Add(new TargetConverter(targets));

			var exercises = Load<Exercise>("exercises", exerciseOptions);

			return exercises;
		}

		static List<T> Load<T>(string file, JsonSerializerOptions? options = null)
		{
			var thisAssembly = Assembly.GetExecutingAssembly();

			using var stream = thisAssembly.GetManifestResourceStream($"WarmupGen.Core.{file}.json") ?? throw new Exception($"Could not find {file}.json");
			using var reader = new StreamReader(stream);

			var data = reader.ReadToEnd();
			var targets = JsonSerializer.Deserialize<List<T>>(data, options) ?? throw new Exception("JSON was invalid");

			return targets;
		}

		static List<Target> LoadTargets()
		{
			return Load<Target>("targets");
		}

		static List<Technique> LoadTechniques()
		{
			return Load<Technique>("techniques");
		}

		public static void WriteJson(ExerciseLibrary lib, string? path = null)
		{
			var techniquePath = Path.Join(path, "techniques.json");
			var targetPath = Path.Join(path, "targets.json");
			var exercisePath = Path.Join(path, "exercises.json");

			JsonSerializerOptions options = new()
			{
				WriteIndented = true
			};

			File.WriteAllText(techniquePath, JsonSerializer.Serialize(lib.Techniques, options));
			File.WriteAllText(targetPath, JsonSerializer.Serialize(lib.Targets, options));

			JsonSerializerOptions exerciseOptions = new()
			{
				WriteIndented = true
			};

			exerciseOptions.Converters.Add(new TechniqueConverter(lib.Techniques));
			exerciseOptions.Converters.Add(new TargetConverter(lib.Targets));

			File.WriteAllText(exercisePath, JsonSerializer.Serialize(lib.Exercises, exerciseOptions));
		}
	}
}