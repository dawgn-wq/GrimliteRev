using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayerAuraValIsLessThan : StatementCommand, IBotCommand
{
	public CmdPlayerAuraValIsLessThan()
	{
		base.Tag = "Player Aura";
		base.Text = "Player Aura's value is less than";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Player.auraComparison("Self", "Less", instance.Value(base.Value1), int.Parse(instance.Value(base.Value2))))
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
		return "Player Aura's value is less than: " + base.Value1 + ", " + base.Value2;
	}
}
