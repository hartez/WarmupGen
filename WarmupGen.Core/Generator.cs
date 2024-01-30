using System.Reflection;
using System.Text.Json;

namespace WarmupGen.Core
{
    public class Generator
    {
        public Generator() 
        { 
        }

        public Generator(IEnumerable<Exercise> exercises) 
        {
            _exercises = new List<Exercise>(exercises);
        }

        public static Warmup GenerateWarmup(int segmentCount)
        {
			var warmup = new Warmup();

            for( int n = 0; n < segmentCount; n++ )
			{ 
				warmup.Add(new Segment(null, null)); 
			}

            return warmup;
        }

        static List<Exercise> ReadJson()
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            using (var stream = thisAssembly.GetManifestResourceStream("WarmupGen.Core.exercises.json"))
            {
                if (stream == null)
                {
                    throw new Exception("Could not find exercises.json");
                }

                using (var reader = new StreamReader(stream))
                {
                    var data = reader.ReadToEnd();
                    var exercises = JsonSerializer.Deserialize<List<Exercise>>(data);

                    if (exercises == null)
                    {
                        throw new Exception("JSON was invalid");
                    }

                    return exercises;
                }
            }
        }

        public static void WriteJson()
        {
            File.WriteAllText("exercises.json", JsonSerializer.Serialize(Exercises));
        }

        static List<Exercise>? _exercises;
        static List<string>? _targets;
        static List<string>? _techniques;

        public static List<Exercise> Exercises => _exercises ??= ReadJson();

        public static List<string> Targets => _targets ??= GetTargets();

		static List<string> GetTargets()
        {
            return Exercises.SelectMany(e => e.Targets, (e, c) => c).OrderBy(c => c).Distinct().ToList();
        }

        public static List<string> Techniques => _techniques ??= GetTechniques();

		static List<string> GetTechniques()
        {
            return Exercises.SelectMany(e => e.Techniques, (e, c) => c).OrderBy(c => c).Distinct().ToList();
        }

        public static Exercise ChooseRandomMatchingExercise(string? technique, string? target, IEnumerable<Exercise>? exclude)
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
}