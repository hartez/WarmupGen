using WarmupGen.Core;

namespace WarmupGen.Tests
{
    public class GeneratorTests
    {
        [Fact]
        public void CanReadJsonData()
        {
            var count = Generator.ExerciseLibrary.Exercises.Count;
            Assert.True(count > 0);
        }

        [Fact]
        public void ExercisesContainsRegularPushups()
        {
            var pushups = Generator.ExerciseLibrary.Exercises.Where(e => e.Name == "Regular Pushups").First();
        }

        [Fact]
        public void TargetsContainsArms()
        {
            var pushups = Generator.ExerciseLibrary.Targets.Where(c => c.Name == "Arms").First();
        }

        [Fact]
        public void TechniquewsContainsStraightPunches()
        {
            var pushups = Generator.ExerciseLibrary.Techniques.Where(t => t.Name == "Straight Punches").First();
        }

        //[Fact(Skip = "Just here to create example json files")]
		[Fact]
        public void Write()
        {
			var targets = new List<Target>()
			{
				new(1, "Core"),
				new(2, "Arms")
			};

			var techniques = new List<Technique>()
			{
				new(1, "Straight Punches"),
				new(2, "Knees")
			};

			var exercises = new List<Exercise>()
			{
				new("Push-ups", [techniques[0]], [targets[1]])
			};

			var lib = new ExerciseLibrary(targets, techniques, exercises);

			Generator.WriteJson(lib);
        }
    }
}