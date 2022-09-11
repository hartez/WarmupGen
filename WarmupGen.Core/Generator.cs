using System.Reflection;
using System.Text.Json;

namespace WarmupGen.Core
{
    public class Generator
    {
        public static Exercise None = new("No Match Found", new(), new());

        public Generator() 
        { 
        }

        public Generator(IEnumerable<Exercise> exercises) 
        {
            _exercises = new List<Exercise>(exercises);
        }

        public Warmup GenerateWarmup(WarmupTemplate template)
        {
            return new Warmup(template.FindMatches(Exercises));
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

        public void WriteJson()
        {
            File.WriteAllText("exercises.json", JsonSerializer.Serialize(Exercises));
        }

        List<Exercise>? _exercises;
        List<string>? _targets;
        List<string>? _techniques;

        public List<Exercise> Exercises => _exercises ??= ReadJson();

        public List<string> Targets => _targets ??= GetTargets();

        List<string> GetTargets()
        {
            return Exercises.SelectMany(e => e.Targets, (e, c) => c).OrderBy(c => c).Distinct().ToList();
        }

        public List<string> Techniques => _techniques ??= GetTechniques();

        List<string> GetTechniques()
        {
            return Exercises.SelectMany(e => e.Techniques, (e, c) => c).OrderBy(c => c).Distinct().ToList();
        }
    }
}