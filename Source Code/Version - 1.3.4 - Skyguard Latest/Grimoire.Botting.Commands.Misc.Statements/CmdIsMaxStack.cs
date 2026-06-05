using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdIsMaxStack : StatementCommand, IBotCommand
{
	public CmdIsMaxStack()
	{
		base.Tag = "Item";
		base.Text = "Is maxed in inventory";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Player.Inventory.ContainsMaxItem(instance.Value(base.Value1)))
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
		return "Item is maxed in inventory: " + base.Value1;
	}
}
