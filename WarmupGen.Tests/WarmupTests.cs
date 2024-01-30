using WarmupGen.Core;

namespace WarmupGen.Tests
{
	public class WarmupTests
	{
		[Fact]
		public void SegmentsGetExerciseWhenAddedToWarmup()
		{
			var segment = new Segment();
			var warmup = new Warmup();
			warmup.Add(segment);

			Assert.NotEqual(Exercise.None, segment.Exercise);
		}

		[Fact]
		public void WarmupCanCreateAndAddSegment()
		{
			var warmup = new Warmup();
			warmup.AddSegment();

			Assert.NotEqual(Exercise.None, warmup.Segments.First().Exercise);
		}

		[Fact]
		public void SegmentCriteriaChangesUpdateExerciseInWarmup()
		{
			var segment = new Segment(null, "Arms");
			var warmup = new Warmup();
			warmup.Add(segment);

			var exercise = segment.Exercise;

			segment.Target = "Legs";

			Assert.NotEqual(exercise, segment.Exercise);
		}

		[Fact]
		public void SegmentCriteriaChangesStopUpdatingWhenRemovedFromWarmup()
		{
			var warmup = new Warmup();
			var segment = new Segment(null, "Arms");

			warmup.Add(segment);

			var exercise = segment.Exercise;

			warmup.Remove(segment);

			segment.Target = "Legs";

			Assert.Equal(exercise, segment.Exercise);
		}

		[Fact]
		public void SegmentsCountsMatch()
		{
			var segment = new Segment();
			var warmup = new Warmup();

			Assert.Empty(warmup.Segments);

			warmup.Add(segment);

			Assert.Single(warmup.Segments);

			warmup.Remove(segment);

			Assert.Empty(warmup.Segments);
		}

		[Fact]
		public void SegmentExerciseDoesNotChangeIfItMatchesNewTarget()
		{
			var warmup = new Warmup();
			var segment = new Segment(null, null);

			warmup.Add(segment);

			var expectedExercise = new Exercise("test1", new() { "technique" }, new() { "target" });
			segment.Exercise = expectedExercise;

			segment.Target = "target";

			// The updated target matches the current exercise, so no need to change it
			Assert.Equal(expectedExercise, segment.Exercise);
		}

		[Fact]
		public void SegmentExerciseDoesNotChangeIfItMatchesNewTechnique()
		{
			var warmup = new Warmup();
			var segment = new Segment(null, null);

			warmup.Add(segment);

			var expectedExercise = new Exercise("test1", new() { "technique" }, new() { "target" });
			segment.Exercise = expectedExercise;

			segment.Technique = "technique";

			// The updated technique matches the current exercise, so no need to change it
			Assert.Equal(expectedExercise, segment.Exercise);
		}
	}
}