namespace WarmupGen.Core
{
	public record Exercise(string Name, List<string> Techniques, List<string> Targets)
    {
		public static Exercise None = new("No Match Found", new(), new());

        public bool Matches(string? technique, string? target)
        {
            if (!string.IsNullOrEmpty(technique))
			{
				if (!Techniques.Contains(technique))
				{
					return false;
				}
			}

			if (!string.IsNullOrEmpty(target))
			{
				if (!Targets.Contains(target))
				{
					return false;
				}
			}

			return true;
        }
    }
}