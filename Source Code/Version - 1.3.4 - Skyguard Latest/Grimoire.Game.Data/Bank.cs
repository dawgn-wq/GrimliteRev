using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Botting;
using Grimoire.Tools;
using Grimoire.UI;

namespace Grimoire.Game.Data;

public class Bank
{
	public List<InventoryItem> SavedItems;

	public List<InventoryItem> Items => Flash.Call<List<InventoryItem>>("GetBankItems", new string[0]);

	public int AvailableSlots => TotalSlots - UsedSlots;

	public int UsedSlots => Flash.Call<int>("UsedBankSlots", new string[0]);

	public int TotalSlots => Flash.Call<int>("BankSlots", new string[0]);

	public bool HasBankOpen => Flash.Call<bool>("HasBankOpen", new object[1] { new bool[0] });

	public bool IsBankLoaded => Flash.Call<bool>("IsBankLoaded", new string[0]);

	public async Task TransferToBank(string itemName)
	{
		if (!Player.Bank.ContainsItem(itemName))
		{
			await BotUtilities.MoveOutOfCombat();
			Flash.Call("Transfer", "Bank", itemName);
			await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.Bank.ContainsItem(itemName), null, 6, 500);
		}
	}

	public async Task TransferFromBank(string itemName)
	{
		if (Player.Bank.ContainsItem(itemName))
		{
			await BotUtilities.MoveOutOfCombat();
			Flash.Call("Transfer", "Inventory", itemName);
			await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !Player.Bank.ContainsItem(itemName), null, 6, 500);
		}
	}

	public async Task Swap(string invItemName, string bankItemName)
	{
		if (ContainsItem(bankItemName) && Player.Inventory.ContainsItem(invItemName))
		{
			await BotUtilities.MoveOutOfCombat();
			Flash.Call("BankSwap", invItemName, bankItemName);
			await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !ContainsItem(bankItemName) && !Player.Inventory.ContainsItem(invItemName), null, 6, 500);
		}
	}

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

	public void Show()
	{
		Flash.Call("ShowBank");
	}

	public void Load()
	{
		Flash.Call("LoadBank");
	}

	public void LoadItems()
	{
		Flash.Call("LoadBankItems");
	}
}
