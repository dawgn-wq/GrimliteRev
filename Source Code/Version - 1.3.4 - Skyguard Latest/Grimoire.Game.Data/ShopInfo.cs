using System.Collections.Generic;
using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Game.Data;

public class ShopInfo
{
	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("bUpgrd")]
	public bool Member { get; set; }

	[JsonProperty("items")]
	public List<InventoryItem> Items { get; set; }

	[JsonProperty("sField")]
	public string Field { get; set; }

	[JsonProperty("ShopID")]
	public int Id { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("bStaff")]
	public bool Staff { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("bHouse")]
	public bool House { get; set; }

	[JsonProperty("sName")]
	public string Name { get; set; }

	[JsonProperty("Location")]
	public string Location { get; set; }

	[JsonProperty("iIndex")]
	public int Index { get; set; }
}
