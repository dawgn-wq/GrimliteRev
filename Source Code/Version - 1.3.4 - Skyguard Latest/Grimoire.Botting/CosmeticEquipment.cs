using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Botting;

public class CosmeticEquipment
{
	private static Dictionary<EquipType, string> _cosMap = new Dictionary<EquipType, string>
	{
		{
			EquipType.Helm,
			"he"
		},
		{
			EquipType.Cape,
			"ba"
		},
		{
			EquipType.Armor,
			"co"
		},
		{
			EquipType.Class,
			"ar"
		},
		{
			EquipType.Pet,
			"pe"
		},
		{
			EquipType.Weapon,
			"Weapon"
		},
		{
			EquipType.Ground,
			"mi"
		}
	};

	private static Dictionary<string, EquipType> _backMap = _cosMap.ToDictionary((KeyValuePair<EquipType, string> kvp) => kvp.Value, (KeyValuePair<EquipType, string> kvp) => kvp.Key);

	public EquipType Slot { get; set; }

	[JsonProperty("ItemID")]
	public int ID { get; set; }

	[JsonProperty("sLink")]
	public string Link { get; set; }

	[JsonProperty("sMeta")]
	public string Meta { get; set; }

	[JsonProperty("sFile")]
	public string SWFFile { get; set; }

	[JsonProperty("sType")]
	public string Type { get; set; }

	public void Equip()
	{
		string text = _cosMap[Slot];
		dynamic val = new ExpandoObject();
		val.sFile = SWFFile;
		val.sLink = Link;
		val.sType = Type;
		val.sMeta = Meta;
		if (ID != 0)
		{
			val.ItemID = ID;
		}
		Flash.Call("SetEquip", new object[2] { text, val });
	}

	public override string ToString()
	{
		return $"{Slot}: {SWFFile};{Link}";
	}

	public static List<CosmeticEquipment> Get(int id)
	{
		Dictionary<string, CosmeticEquipment> source = JsonConvert.DeserializeObject<Dictionary<string, CosmeticEquipment>>(Flash.Call("GetEquip", id)) ?? new Dictionary<string, CosmeticEquipment>();
		return (from kvp in source
			select ((kvp.Value.Slot = (_backMap.TryGetValue(kvp.Key, out var value) ? value : EquipType.None)) == EquipType.None) ? null : kvp.Value into x
			where x != null
			select x).ToList();
	}
}
