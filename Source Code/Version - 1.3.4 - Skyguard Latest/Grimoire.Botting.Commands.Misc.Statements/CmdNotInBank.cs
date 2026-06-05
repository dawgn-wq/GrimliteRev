using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdNotInBank : StatementCommand, IBotCommand
{
	public CmdNotInBank()
	{
		base.Tag = "Item";
		base.Text = "Is not in bank";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.Bank.ContainsItem(instance.Value(base.Value1), instance.Value(base.Value2)))
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
		return "Item is not in bank: " + base.Value1 + ", " + base.Value2;
	}
}
