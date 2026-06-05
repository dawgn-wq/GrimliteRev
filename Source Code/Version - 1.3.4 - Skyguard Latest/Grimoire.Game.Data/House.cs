using System;
using System.Collections.Generic;
using System.Linq;
using Grimoire.Tools;

namespace Grimoire.Game.Data;

public class House
{
	public List<InventoryItem> Items => Flash.Call<List<InventoryItem>>("GetHouseItems", new string[0]);

	public int TotalSlots => Flash.Call<int>("HouseSlots", new string[0]);

	public bool ContainsItem(string itemName, string quantity = "*")
	{
		InventoryItem ınventoryItem = Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
		if (ınventoryItem != null)
		{
			if (!(quantity == "*"))
			{
				return ınventoryItem.Quantity >= int.Parse(quantity);
			}
			return true;
		}
		return false;
	}
}
