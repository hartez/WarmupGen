using System.Text.Json.Serialization;

namespace WarmupGen.Core
{
	public record Exercise(string Name, List<Technique> Techniques, List<Target> Targets)
    {
		public static Exercise None = new("No Match Found", [], []);

        public bool Matches(string? technique, string? target)
        {
			// TODO Originally targets/techniques didn't have IDs; this matching would be a bit faster if we used IDs
		
			if (!string.IsNullOrEmpty(technique) && !Techniques.Any(t => t.Name == technique))
			{
				return false;
			}

			if (!string.IsNullOrEmpty(target) && !Targets.Any(t => t.Name == target))
			{
				return false;
			}

			return true;
        }

		[JsonIgnore]
		public string TechniquesDisplay => string.Join(", ", Techniques.Select(t => t.Name).Order());

		[JsonIgnore]
		public string TargetsDisplay => string.Join(", ", Targets.Select(t => t.Name).Order());
    }
}