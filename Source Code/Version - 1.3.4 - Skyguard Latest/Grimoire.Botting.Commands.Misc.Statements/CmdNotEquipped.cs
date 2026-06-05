using System;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdNotEquipped : StatementCommand, IBotCommand
{
	public CmdNotEquipped()
	{
		base.Tag = "Item";
		base.Text = "Is not equipped";
	}

	public Task Execute(IBotEngine instance)
	{
		if ((Player.Inventory.Items.Find((InventoryItem x) => x.Name.Equals(instance.Value(base.Value1), StringComparison.OrdinalIgnoreCase)) ?? new InventoryItem()).IsEquipped)
		{
			instance.Index++;
		}
		return Task.FromResult<object>(null);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Item is not equipped: " + base.Value1;
	}
}
