using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdIsNotMember : StatementCommand, IBotCommand
{
	public CmdIsNotMember()
	{
		base.Tag = "This player";
		base.Text = "This player is not Member";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.IsMember)
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
		return "This player is not Member";
	}
}
