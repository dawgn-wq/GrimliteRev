using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayerIsNotInMyCell : StatementCommand, IBotCommand
{
	public CmdPlayerIsNotInMyCell()
	{
		base.Tag = "Player";
		base.Text = "Player is not in my cell";
	}

	public Task Execute(IBotEngine instance)
	{
		try
		{
			if (World.AvatarsInMyCell.FirstOrDefault((Avatar p) => p.Username.Equals(instance.Value(base.Value1), StringComparison.OrdinalIgnoreCase)) != null)
			{
				instance.Index++;
			}
		}
		catch
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
		return "Player is not in my cell: " + base.Value1;
	}
}
