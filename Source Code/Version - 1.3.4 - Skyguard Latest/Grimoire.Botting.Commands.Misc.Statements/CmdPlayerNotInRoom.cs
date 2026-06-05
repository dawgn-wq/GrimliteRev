using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayerNotInRoom : StatementCommand, IBotCommand
{
	public CmdPlayerNotInRoom()
	{
		base.Tag = "Player";
		base.Text = "Player is not in room";
	}

	public Task Execute(IBotEngine instance)
	{
		if (World.PlayersInMap.FirstOrDefault((string p) => p.Equals(instance.Value(base.Value1), StringComparison.OrdinalIgnoreCase)) != null)
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
		return "Player is not in room: " + base.Value1;
	}
}
