using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayerAuraValIsNot : StatementCommand, IBotCommand
{
	public CmdPlayerAuraValIsNot()
	{
		base.Tag = "Player Aura";
		base.Text = "Player Aura's value is not";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.auraComparison("Self", "Equal", instance.Value(base.Value1), int.Parse(instance.Value(base.Value2))))
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
		return "Player Aura's value is not: " + base.Value1 + ", " + base.Value2;
	}
}
