using System.Collections.Generic;
using Newtonsoft.Json;

namespace Grimoire.Game.Data;

public class Cell
{
	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("pads")]
	public List<string> Pads { get; set; }

	[JsonProperty("mapItems")]
	public List<MapItem> MapItems { get; set; }
}
