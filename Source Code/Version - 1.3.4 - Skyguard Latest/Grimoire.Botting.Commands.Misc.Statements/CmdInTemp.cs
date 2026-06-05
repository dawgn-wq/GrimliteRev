using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdInTemp : StatementCommand, IBotCommand
{
	public CmdInTemp()
	{
		base.Tag = "Item";
		base.Text = "Is in temp inventory";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Player.TempInventory.ContainsItem(instance.Value(base.Value1), instance.Value(base.Value2)))
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
		return "Item is in temp inventory: " + base.Value1 + ", " + base.Value2;
	}
}
