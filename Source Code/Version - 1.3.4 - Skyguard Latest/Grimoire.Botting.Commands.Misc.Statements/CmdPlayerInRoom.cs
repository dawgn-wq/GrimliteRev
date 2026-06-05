using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayerInRoom : StatementCommand, IBotCommand
{
	public CmdPlayerInRoom()
	{
		base.Tag = "Player";
		base.Text = "Player is in room";
	}

	public Task Execute(IBotEngine instance)
	{
		if (World.PlayersInMap.FirstOrDefault((string p) => p.Equals(instance.Value(base.Value1), StringComparison.OrdinalIgnoreCase)) == null)
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
		return "Player is in room: " + base.Value1;
	}
}
