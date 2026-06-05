using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdTargetAuraValIsLessThan : StatementCommand, IBotCommand
{
	public CmdTargetAuraValIsLessThan()
	{
		base.Tag = "Target Aura";
		base.Text = "Target Aura's value is less than";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Player.auraComparison("Target", "Less", instance.Value(base.Value1), int.Parse(instance.Value(base.Value2))))
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
		return "Target Aura's value is less than: " + base.Value1 + ", " + base.Value2;
	}
}
