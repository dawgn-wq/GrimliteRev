using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers;

public class HandlerDrops : IJsonMessageHandler
{
	public JToken items;

	public InventoryItem drop;

	public string[] HandledCommands { get; } = new string[3] { "dropItem", "getDrop", "addItems" };

	public async void Handle(JsonMessage message)
	{
		switch (message.Command)
		{
		case "dropItem":
			items = message.DataObject?["items"];
			if (items != null)
			{
				drop = items.ToObject<Dictionary<int, InventoryItem>>().First().Value;
				World.OnItemDropped(drop);
			}
			break;
		case "getDrop":
			await Task.Delay(10);
			ParseLog(message, manual: true);
			break;
		case "addItems":
			ParseLog(message, manual: false);
			break;
		}
	}

	void IJsonMessageHandler.Handle(JsonMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}

	private void ParseLog(JsonMessage content, bool manual)
	{
		if (manual)
		{
			if (content.DataObject["bSuccess"].Value<int>() != 1)
			{
				return;
			}
			int id = content.DataObject["ItemID"].Value<int>();
			World.DropStack.RemoveAll(id);
			if (!BotManager.Instance.ActiveBotEngine.IsRunning)
			{
				return;
			}
			bool flag = content.DataObject["bBank"].Value<int>() != 0;
			int num = content.DataObject["iQty"].Value<int>();
			if (flag)
			{
				Player.Bank.SavedItems.Find((InventoryItem i) => i.Id.Equals(id)).Quantity += num;
			}
			drop = (flag ? Player.Bank.SavedItems : Player.Inventory.Items).FirstOrDefault((InventoryItem i) => i.Id.Equals(id)) ?? Player.House.Items.FirstOrDefault((InventoryItem i) => i.Id.Equals(id));
			LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Item accepted: {drop.Name} x {num}.\r\n");
			if (BotManager.Instance.chkNotifyItemOnPickup.Checked && BotManager.Instance.ActiveBotEngine.Configuration.Items.Any((string i) => i.Equals(drop.Name, StringComparison.OrdinalIgnoreCase)))
			{
				World.GamePopup(string.Format("{0}: {1} {2}/{3}", flag ? "Bank" : "Inventory", drop.Name, drop.Quantity, drop.MaxStack));
			}
			return;
		}
		items = content.DataObject?["items"];
		if (items == null)
		{
			return;
		}
		foreach (JProperty item in (IEnumerable<JToken>)items)
		{
			drop = Player.Inventory.Items?.FirstOrDefault((InventoryItem i) => i.Id.Equals(int.Parse(item.Name)));
			if (drop == null)
			{
				continue;
			}
			string text = item.Value["iQty"].ToString();
			string text2 = item.Value["iQtyNow"].ToString();
			if (BotManager.Instance.ActiveBotEngine.IsRunning)
			{
				LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Item accepted: {drop.Name} x {text}.\r\n");
				if (BotManager.Instance.chkNotifyItemOnPickup.Checked && BotManager.Instance.ActiveBotEngine.Configuration.Items.Any((string i) => i.Equals(drop.Name, StringComparison.OrdinalIgnoreCase)))
				{
					World.GamePopup($"Inventory: {drop.Name} {text2}/{drop.MaxStack}");
				}
			}
			if (!Bot.DropsInInventory.Contains(drop.Name))
			{
				Bot.DropsInInventory.Add(drop.Name);
			}
			LogForm.Instance.AppendDrops($"[Item Drop] ({text2}) {drop.Name} x {text} at {DateTime.Now:hh:mm:ss tt}.\r\n");
		}
	}
}
