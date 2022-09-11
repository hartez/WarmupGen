using WarmupGen.Core;

namespace WarmupGen.Tests
{
    public class SegmentTemplateTests
    {
        public static IEnumerable<object[]> SegmentTestData() 
        {
            var exerciseWithCategory = new Exercise("test1", new(), new() { "category" });
            var exerciseWrongCategory = new Exercise("test1", new(), new() { "notCategory" });
            var exerciseWithTechnique = new Exercise("test2", new() { "technique" }, new());
            var exerciseWrongTechnique = new Exercise("test2", new() { "notTechnique" }, new());

            var exerciseWithCategoryAndTechnique = new Exercise("test3", new() { "technique" }, new() { "category" });

            var requiresCategory = new SegmentTemplate(null, "category");
            var requiresTechnique = new SegmentTemplate("technique", null);

            var requiresCategoryAndTechnique = new SegmentTemplate("technique", "category");

            var any = new SegmentTemplate();

            yield return new object[] { any, exerciseWithCategory, true };
            yield return new object[] { any, exerciseWithTechnique, true };

            yield return new object[] { requiresCategory, exerciseWithCategory, true };
            yield return new object[] { requiresCategory, exerciseWrongCategory, false };
            yield return new object[] { requiresCategory, exerciseWithTechnique, false };

            yield return new object[] { requiresTechnique, exerciseWithTechnique, true };
            yield return new object[] { requiresTechnique, exerciseWithCategory, false };
            yield return new object[] { requiresTechnique, exerciseWrongTechnique, false };

            yield return new object[] { requiresCategoryAndTechnique, exerciseWithCategoryAndTechnique, true };
            yield return new object[] { requiresCategoryAndTechnique, exerciseWithCategory, false };
            yield return new object[] { requiresCategoryAndTechnique, exerciseWithTechnique, false };
        }

        [Theory, MemberData(nameof(SegmentTestData))]
        public void SegmentTemplateMatches(SegmentTemplate template, Exercise exercise, bool isMatch)
        {
            Assert.Equal(isMatch, template.Matches(exercise));
        }
    }

    public class WarmupTemplateTests 
    {
        [Fact]
        public void CanGenerateAWarmup() 
        {
            var e1 = new Exercise("test1", new(), new() { "category" });
            var e2 = new Exercise("test2", new() { "technique" }, new());

            var exercises = new List<Exercise>() { e1, e2 };

            var template = new WarmupTemplate(2);
            template.SegmentTemplates[0].Target = "category";
            template.SegmentTemplates[1].Technique = "technique";

            var result = template.FindMatches(exercises);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void NoMatchReturnsNone()
        {
            var e1 = new Exercise("test1", new(), new() { "category" });
            var e2 = new Exercise("test2", new() { "technique" }, new());

            var exercises = new List<Exercise>() { e1, e2 };

            var template = new WarmupTemplate(1);
            template.SegmentTemplates[0].Target = "nomatch";

            var result = template.FindMatches(exercises);
            Assert.Single(result);
            Assert.Equal(Generator.None, result[0]);
        }
    }
}