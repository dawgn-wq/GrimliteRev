using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking;

public class JsonMessage : Message
{
	public JToken Object { get; }

	public JToken DataObject => Object?["b"]?["o"];

	public JsonMessage(string raw)
	{
		try
		{
			base.RawContent = raw;
			Object = JObject.Parse(raw);
			base.Command = DataObject?["cmd"]?.Value<string>();
		}
		catch (JsonReaderException)
		{
		}
	}

	public override string ToString()
	{
		return Object.ToString(Formatting.None);
	}
}
