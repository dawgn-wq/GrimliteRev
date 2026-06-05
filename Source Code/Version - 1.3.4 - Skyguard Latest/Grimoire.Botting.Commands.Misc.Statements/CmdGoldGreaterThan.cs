using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdGoldGreaterThan : StatementCommand, IBotCommand
{
	public CmdGoldGreaterThan()
	{
		base.Tag = "This player";
		base.Text = "Gold is greater than";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.Gold <= int.Parse(instance.Value(base.Value1)))
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
		return "Gold is greater than: " + base.Value1;
	}
}
