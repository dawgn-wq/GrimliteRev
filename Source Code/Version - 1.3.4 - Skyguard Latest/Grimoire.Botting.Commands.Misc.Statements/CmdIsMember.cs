using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdIsMember : StatementCommand, IBotCommand
{
	public CmdIsMember()
	{
		base.Tag = "This player";
		base.Text = "This player is Member";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Player.IsMember)
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
		return "This player is Member";
	}
}
