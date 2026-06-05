using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdMonsterNotInRoom : StatementCommand, IBotCommand
{
	public CmdMonsterNotInRoom()
	{
		base.Tag = "Monster";
		base.Text = "Is not in room";
	}

	public Task Execute(IBotEngine instance)
	{
		if (World.IsMonsterAvailable(instance.Value(base.Value1)))
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
		return "Monster is not in room: " + base.Value1;
	}
}
