namespace WarmupGen.Core
{
    public class Warmup
    {
        public List<Exercise> Exercises { get; }

        public Warmup(IEnumerable<Exercise> exercises)
        {
            Exercises = new(exercises);
        }
    }
}