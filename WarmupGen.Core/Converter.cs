using System.Text.Json;
using System.Text.Json.Serialization;

namespace WarmupGen.Core
{
	public class Converter<T>(List<T> things) : JsonConverter<T> where T : IReferenceable
	{
		private readonly Dictionary<int, T> _targets = things.ToDictionary(t => t.Id, t => t);

		public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var id = reader.GetInt32();

			if (_targets.TryGetValue(id, out T? target))
			{
				return target;
			}

			return default;
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue(value.Id);
		}
	}

	public class TechniqueConverter(List<Technique> things) : Converter<Technique>(things)
	{
	}

	public class TargetConverter(List<Target> things) : Converter<Target>(things)
	{
	}

	public interface IReferenceable { public int Id { get; } }
}