using System.Collections.Generic;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Game;

public class ExtendedAvatar
{
	[JsonProperty("UserID")]
	public int UserID { get; set; }

	[JsonProperty("CharID")]
	public int CharID { get; set; }

	public int Gold { get; set; }

	[JsonProperty("intCoins")]
	public int AdventureCoin { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("iBoostRep")]
	public bool BoostRep { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("iBoostG")]
	public bool BoostGold { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("iBoostXP")]
	public bool BoostXP { get; set; }

	[JsonConverter(typeof(BoolConverter))]
	[JsonProperty("iBoostCP")]
	public bool BoostCP { get; set; }

	public List<Faction> Factions { get; set; }

	public List<InventoryItem> Inventory { get; set; }

	public List<InventoryItem> TemporaryInventory { get; set; }

	public List<InventoryItem> HouseInventory { get; set; }

	public List<InventoryItem> Bank { get; set; }

	[JsonProperty("iBagSlots")]
	public int InventorySlots { get; set; }

	public int UsedInventorySlots { get; set; }

	[JsonProperty("iBankSlots")]
	public int BankSlots { get; set; }

	[JsonProperty("bankCount")]
	public int UsedBankSlots { get; set; }

	[JsonProperty("iHouseSlots")]
	public int HouseSlots { get; set; }

	public int UsedHouseSlots { get; set; }

	[JsonProperty("intActivationFlag")]
	public int ActivationFlag { get; set; }
}
