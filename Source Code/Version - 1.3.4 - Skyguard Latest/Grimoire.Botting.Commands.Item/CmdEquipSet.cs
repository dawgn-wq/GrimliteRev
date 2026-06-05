using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using Newtonsoft.Json;

namespace Grimoire.Botting.Commands.Item;

public class CmdEquipSet : IBotCommand
{
	private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
	{
		DefaultValueHandling = DefaultValueHandling.Include,
		TypeNameHandling = TypeNameHandling.All
	};

	public string ItemName { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		dynamic val = JsonConvert.DeserializeObject<SetItem>(Application.StartupPath + ItemName, _serializerSettings);
		foreach (dynamic obj in val.Set)
		{
			InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(obj.Name, StringComparison.OrdinalIgnoreCase) && i.IsEquippable);
			while (instance.IsRunning && !IsEquipped(item.Id))
			{
				BotData.BotState = BotData.State.Transaction;
				while (instance.IsRunning && Player.CurrentState == Player.State.InCombat)
				{
					Player.MoveToCell(Player.Cell, Player.Pad);
					await Task.Delay(1000);
				}
				await instance.WaitUntil(() => World.IsActionAvailable(LockActions.EquipItem));
				Player.Equip(item.Name);
			}
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public bool IsEquipped(int ItemID)
	{
		return Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.IsEquipped && it.Id == ItemID) != null;
	}

	public override string ToString()
	{
		return "Equip Set: " + ItemName;
	}
}
