namespace WarmupGen.Core
{
    public class SegmentTemplate 
    { 
        public string? Technique { get; set; }
        public string? Target { get; set; }

        public SegmentTemplate() { }

        public SegmentTemplate(string? technnique, string? category)
        {
            Technique = technnique;
            Target = category;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Technique) && string.IsNullOrEmpty(Target)) 
            {
                return "Any Exercise";
            }

            var category = string.IsNullOrEmpty(Target) ? "Any" : Target;
            var technique = string.IsNullOrEmpty(Technique) ? "Any" : Technique;

            return $"Target: {category}, Technique: {technique}";
        }

        public bool Matches(Exercise exercise)
        {
            if (!string.IsNullOrEmpty(Technique)) 
            {
                if (!exercise.Techniques.Contains(Technique)) 
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(Target))
            {
                if (!exercise.Targets.Contains(Target))
                {
                    return false;
                }
            }

            return true;
        }
    }
}