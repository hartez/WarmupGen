namespace WarmupGen.Core
{
    public class WarmupTemplate 
    {
        public List<SegmentTemplate> SegmentTemplates { get; private set; }

        public int Count => SegmentTemplates.Count;

        public WarmupTemplate(int count) 
        {
            SegmentTemplates = new List<SegmentTemplate>(count);
            for (int n = 0; n < count; n++) 
            {
                SegmentTemplates.Add(new SegmentTemplate());
            }
        }

        public List<Exercise> FindMatches(List<Exercise> exercises) 
        {
            List<Exercise> candidates = new(exercises);
            var result = new List<Exercise>(Count);

            for (int n = 0; n < Count; n++)
            {
                var segment = Choose(SegmentTemplates[n], candidates);

                if (segment != Generator.None) 
                {
                    candidates.Remove(segment);
                }

                result.Add(segment);
            }

            return result;
        }

        static Exercise Choose(SegmentTemplate template, List<Exercise> exercises) 
        {
            var candidates = exercises.Where(e => template.Matches(e)).ToList();

            var count = candidates.Count;
            if (count == 0) 
            {
                return Generator.None;
            }

            var random = new Random(DateTime.Now.Millisecond);
            var index = random.Next(count - 1);

            return candidates[index];
        }
    }
}