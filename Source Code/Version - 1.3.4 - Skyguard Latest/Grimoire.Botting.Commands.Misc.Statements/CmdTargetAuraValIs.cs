using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdTargetAuraValIs : StatementCommand, IBotCommand
{
	public CmdTargetAuraValIs()
	{
		base.Tag = "Target Aura";
		base.Text = "Target Aura's value is";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Player.auraComparison("Target", "Equal", instance.Value(base.Value1), int.Parse(instance.Value(base.Value2))))
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
		return "Target Aura's value is: " + base.Value1 + ", " + base.Value2;
	}
}
