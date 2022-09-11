using WarmupGen.Core;

namespace WarmupGen.Tests
{
    public class GeneratorTests
    {
        [Fact]
        public void CanReadJsonData()
        {
            var generator = new Generator();
            var count = generator.Exercises.Count;
            Assert.True(count > 0);
        }

        [Fact]
        public void ExercisesContainsRegularPushups()
        {
            var generator = new Generator();
            var pushups = generator.Exercises.Where(e => e.Name == "Regular Pushups").First();
        }

        [Fact]
        public void CategoriesContainsPushups()
        {
            var generator = new Generator();
            var pushups = generator.Targets.Where(c => c == "Pushups").First();
        }

        [Fact]
        public void TechniquewsContainsStraightPunches()
        {
            var generator = new Generator();
            var pushups = generator.Techniques.Where(t => t == "Straight Punches").First();
        }

        [Fact]
        public void WarmupCountMatchesTemplateCount()
        {
            var generator = new Generator();
            var template = new WarmupTemplate(3);
            var warmup = generator.GenerateWarmup(template);

            Assert.Equal(template.Count, warmup.Exercises.Count);
        }

        [Fact(Skip = "Just here to create example json files")]
        public void Write()
        {
            var generator = new Generator();
            generator.WriteJson();
        }
    }
}