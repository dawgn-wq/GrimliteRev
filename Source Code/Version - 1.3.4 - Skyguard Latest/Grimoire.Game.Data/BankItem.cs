using System.Collections.Generic;
using System.Linq;
using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Game.Data;

public class BankItem
{
	public static readonly string[] EquippableCategories = new string[19]
	{
		"Sword", "Axe", "Dagger", "Gauntlet", "Gun", "HandGun", "Bow", "Mace", "Polearm", "Rifle",
		"Staff", "Wand", "Whip", "Class", "Armor", "Helm", "Cape", "Misc", "Item"
	};

	public static readonly string[] Weapons = new string[13]
	{
		"Sword", "Axe", "Dagger", "Gauntlet", "Gun", "HandGun", "Bow", "Mace", "Polearm", "Rifle",
		"Staff", "Wand", "Whip"
	};

	public static readonly string[] EquippableNonWeapon = new string[6] { "Class", "Armor", "Helm", "Cape", "Pet", "Misc" };

	public static readonly Dictionary<int, string> Enhancement = new Dictionary<int, string>
	{
		{ 0, "Unenhanced" },
		{ 6, "Mage" },
		{ 9, "Lucky" }
	};

	[JsonProperty("EnhLvl")]
	public int EnhLevel { get; set; }

	[JsonConverter(typeof(IntStringConverter))]
	[JsonProperty("EnhID")]
	public int EnhID { get; set; }

	public string EnhancementType => Enhancement[EnhID];

	[JsonProperty("EnhPatternID")]
	public int EnhPatternId { get; set; }

	[JsonProperty("CharItemID")]
	public float CharItemID { get; set; }

	[JsonProperty("EnhPatternID")]
	public string EnhPatternID { get; set; }

	[JsonProperty("EnhRng")]
	public string EnhRng { get; set; }

	[JsonProperty("EnhRty")]
	public string EnhRty { get; set; }

	[JsonProperty("ItemID")]
	public string ItemID { get; set; }

	[JsonProperty("bBank")]
	public string Bank { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("bEquip")]
	public bool Equipped { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("bStaff")]
	public bool Staff { get; set; }

	[JsonProperty("iDPS")]
	public string DPS { get; set; }

	[JsonProperty("EnhDPS")]
	public int EnhDPS { get; set; }

	[JsonProperty("iEnh")]
	public int iEnh { get; set; }

	[JsonProperty("iRng")]
	public string Rng { get; set; }

	[JsonProperty("iRty")]
	public int Rty { get; set; }

	[JsonProperty("sES")]
	public string ES { get; set; }

	[JsonProperty("sIcon")]
	public string Icon { get; set; }

	[JsonProperty("sElmt")]
	public string Element { get; set; }

	[JsonProperty("sMeta")]
	public string Meta { get; set; }

	[JsonProperty("iLvl")]
	public int Level { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("bCoins")]
	public bool IsAcItem { get; set; }

	public int CharItemId { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("bEquip")]
	public bool IsEquipped { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("bUpg")]
	public bool IsMemberOnly { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("bTemp")]
	public bool IsTemporary { get; set; }

	[JsonProperty("iCost")]
	public int Cost { get; set; }

	[JsonProperty("ItemID")]
	public int Id { get; set; }

	[JsonProperty("ShopItemID")]
	public int ShopItemId { get; set; }

	[JsonProperty("iRate")]
	public string DropChance { get; set; }

	public bool IsEquippable => EquippableCategories.Contains(Category);

	public bool IsWeapon => Weapons.Contains(Category);

	public bool IsEquippableNonItem => EquippableNonWeapon.Contains(Category);

	public bool ShouldSerializeDescription => false;

	public bool ShouldSerializeMaxStack => false;

	public bool ShouldSerializeLevel => false;

	public bool ShouldSerializeIsAcItem => false;

	public bool ShouldSerializeLink => false;

	public bool ShouldSerializeFile => false;

	public bool ShouldSerializeIsEquipped => false;

	public bool ShouldSerializeIsMemberOnly => false;

	[JsonProperty("ItemID")]
	public virtual int ID { get; set; }

	[JsonProperty("sName")]
	[JsonConverter(typeof(TrimConverter))]
	public virtual string Name { get; set; }

	[JsonProperty("sDesc")]
	public virtual string Description { get; set; }

	[JsonProperty("iQty")]
	public virtual int Quantity { get; set; }

	[JsonProperty("iStk")]
	public virtual int MaxStack { get; set; }

	[JsonProperty("bUpg")]
	[JsonConverter(typeof(StringBoolConverter))]
	public virtual bool Upgrade { get; set; }

	[JsonProperty("bCoins")]
	[JsonConverter(typeof(StringBoolConverter))]
	public virtual bool Coins { get; set; }

	[JsonProperty("sType")]
	public virtual string Category { get; set; }

	[JsonProperty("bTemp")]
	[JsonConverter(typeof(StringBoolConverter))]
	public virtual bool Temp { get; set; }

	[JsonProperty("sFile")]
	public virtual string File { get; set; }

	[JsonProperty("sLink")]
	public virtual string Link { get; set; }

	public bool ShouldSerializeIsTemporary()
	{
		return false;
	}

	public bool ShouldSerializeCost()
	{
		return false;
	}

	public bool ShouldSerializeCategory()
	{
		return false;
	}

	public bool ShouldSerializeShopItemId()
	{
		return false;
	}

	public bool ShouldSerializeDropChance()
	{
		return false;
	}
}
