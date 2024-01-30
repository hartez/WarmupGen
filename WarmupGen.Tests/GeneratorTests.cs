using WarmupGen.Core;

namespace WarmupGen.Tests
{
    public class GeneratorTests
    {
        [Fact]
        public void CanReadJsonData()
        {
            var count = Generator.Exercises.Count;
            Assert.True(count > 0);
        }

        [Fact]
        public void ExercisesContainsRegularPushups()
        {
            var pushups = Generator.Exercises.Where(e => e.Name == "Regular Pushups").First();
        }

        [Fact]
        public void TargetsContainsArms()
        {
            var pushups = Generator.Targets.Where(c => c == "Arms").First();
        }

        [Fact]
        public void TechniquewsContainsStraightPunches()
        {
            var pushups = Generator.Techniques.Where(t => t == "Straight Punches").First();
        }

        [Fact(Skip = "Just here to create example json files")]
        public void Write()
        {
			Generator.WriteJson();
        }
    }
}