using WarmupGen.Core;

namespace WarmupGen.Tests
{
	public class ExerciseMatchingTests
	{
		public static TheoryData<string?, string?, Exercise, bool> SegmentTestData()
		{
			var exerciseWithTarget = new Exercise("test1", new(), new() { "target" });
			var exerciseWrongTarget = new Exercise("test1", new(), new() { "notTarget" });
			var exerciseWithTechnique = new Exercise("test2", new() { "technique" }, new());
			var exerciseWrongTechnique = new Exercise("test2", new() { "notTechnique" }, new());

			var exerciseWithTargetAndTechnique = new Exercise("test3", new() { "technique" }, new() { "target" });

			return new TheoryData<string?, string?, Exercise, bool>()
			{
				{ null, "target", exerciseWithTarget, true },
				{ null, "target", exerciseWithTechnique, false },
				{ null, "target", exerciseWithTarget, true },
				{ null, "target", exerciseWrongTarget, false },
				{ null, "target", exerciseWithTechnique, false },
				{ "technique", null, exerciseWithTechnique, true },
				{ "technique", null, exerciseWithTarget, false },
				{ "technique", null, exerciseWrongTechnique, false },
				{ "technique", "target", exerciseWithTargetAndTechnique, true },
				{ "technique", "target", exerciseWithTarget, false },
				{ "technique", "target", exerciseWithTechnique, false }
			};
		}

		[Theory, MemberData(nameof(SegmentTestData))]
		public void SegmentTemplateMatches(string? technique, string? target, Exercise exercise, bool expected)
		{
			Assert.Equal(expected, exercise.Matches(technique, target));
		}
	}
}