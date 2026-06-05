using Newtonsoft.Json;

namespace Grimoire.Game.Data;

public class MapItem
{
	[JsonProperty("ID")]
	public int ID { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("qID")]
	public int QuestID { get; set; }

	[JsonProperty("itemName")]
	public string ProcessMsg { get; set; }

	[JsonProperty("collectMsg")]
	public string CollectMsg { get; set; }
}
