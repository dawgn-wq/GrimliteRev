using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Item;

public class CmdEquip : IBotCommand
{
	public string ItemName { get; set; }

	public bool Safe { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(instance.Value(ItemName), StringComparison.OrdinalIgnoreCase) && i.IsEquippable);
		if (item != null && !item.IsEquipped)
		{
			BotData.BotState = BotData.State.Transaction;
			if (Safe)
			{
				await BotUtilities.MoveOutOfCombat();
			}
			await item.Equip();
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return (Safe ? "Safe " : null) + "Equip: " + ItemName;
	}
}
