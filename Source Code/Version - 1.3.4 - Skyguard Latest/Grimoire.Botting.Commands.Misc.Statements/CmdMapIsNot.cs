using System;
using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdMapIsNot : StatementCommand, IBotCommand
{
	public CmdMapIsNot()
	{
		base.Tag = "Map";
		base.Text = "Map is not";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.Map.Equals(instance.Value(base.Value1).Contains("-") ? instance.Value(base.Value1).Split('-')[0] : instance.Value(base.Value1), StringComparison.OrdinalIgnoreCase))
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
		return "Map is not: " + base.Value1;
	}
}
