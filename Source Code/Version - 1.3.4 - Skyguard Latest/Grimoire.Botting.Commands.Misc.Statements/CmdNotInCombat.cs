using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdNotInCombat : StatementCommand, IBotCommand
{
	public CmdNotInCombat()
	{
		base.Tag = "This player";
		base.Text = "Not in combat";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.CurrentState == Player.State.InCombat)
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
		return "This player is not in combat state";
	}
}
