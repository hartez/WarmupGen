using System.Diagnostics;
using System.Text;

namespace WarmupGen.Core
{
    public record Exercise(string Name, List<string> Techniques, List<string> Targets);
}