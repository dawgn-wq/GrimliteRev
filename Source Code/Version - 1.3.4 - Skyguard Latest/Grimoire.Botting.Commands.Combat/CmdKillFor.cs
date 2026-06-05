using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Combat;

public class CmdKillFor : IBotCommand
{
	public string Monster { get; set; }

	public string ItemName { get; set; }

	public ItemType ItemType { get; set; }

	public string Quantity { get; set; }

	private bool notInInventory(string item, string quantity)
	{
		if (ItemType == ItemType.Items)
		{
			return !Player.Inventory.ContainsItem(item, quantity);
		}
		return !Player.TempInventory.ContainsItem(item, quantity);
	}

	public async Task Execute(IBotEngine instance)
	{
		CmdKill kill = new CmdKill
		{
			Monster = instance.Value(Monster)
		};
		while (instance.IsRunning && notInInventory(instance.Value(ItemName), instance.Value(Quantity)))
		{
			await kill.Execute(instance);
		}
		Player.CancelTarget();
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Kill for " + ((ItemType == ItemType.Items) ? "items" : "temporary items") + ": " + Monster;
	}
}
