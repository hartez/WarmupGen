namespace WarmupGen.Core
{
	public class Warmup
	{
		public IReadOnlyList<Segment> Segments { get => _segments; }

		private readonly List<Segment> _segments = new();

		public void Add(Segment segment)
		{
			_segments.Add(segment);
			segment.CriteriaChanged += SegmentCriteriaChanged;
			segment.Exercise = ChooseRandomMatchingExerciseFor(segment);
		}

		public void AddSegment()
		{
			var segment = new Segment();
			Add(segment);
		}

		public void Remove(Segment segment)
		{
			_segments.Remove(segment);
			segment.CriteriaChanged -= SegmentCriteriaChanged;
		}

		private void SegmentCriteriaChanged(object? sender, EventArgs e)
		{
			if (sender is not Segment segment)
			{
				return;
			}

			if (segment.Exercise.Matches(segment.Technique, segment.Target))
			{
				return;
			}

			segment.Exercise = ChooseRandomMatchingExerciseFor(segment);
		}

		public Exercise ChooseRandomMatchingExerciseFor(Segment segment)
		{
			var currentExercises = Segments.Where(s => s != segment).Select(s => s.Exercise);

			return Generator.ChooseRandomMatchingExercise(segment.Technique, segment.Target, currentExercises);
		}
	}
}