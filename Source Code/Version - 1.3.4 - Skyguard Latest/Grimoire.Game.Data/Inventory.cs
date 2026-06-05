using System;
using System.Collections.Generic;
using System.Linq;
using Grimoire.Tools;

namespace Grimoire.Game.Data;

public class Inventory
{
	public List<InventoryItem> Items => Flash.Call<List<InventoryItem>>("GetInventoryItems", new string[0]);

	public int MaxSlots => Flash.Call<int>("InventorySlots", new string[0]);

	public int UsedSlots => Flash.Call<int>("UsedInventorySlots", new string[0]);

	public int AvailableSlots => MaxSlots - UsedSlots;

	public bool ContainsItem(string name, string quantity = "*")
	{
		InventoryItem ınventoryItem = Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
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

	public bool ContainsMaxItem(string name)
	{
		InventoryItem ınventoryItem = Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
		if (ınventoryItem != null)
		{
			return ınventoryItem.Quantity >= ınventoryItem.MaxStack;
		}
		return false;
	}
}
