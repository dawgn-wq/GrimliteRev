using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdTargetAuraStringValIsNot : StatementCommand, IBotCommand
{
	public CmdTargetAuraStringValIsNot()
	{
		base.Tag = "Target Aura";
		base.Text = "Target Aura's value is not (string)";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.isAuraWithStrValActive("Target", instance.Value(base.Value1), instance.Value(base.Value2)))
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
		return "Target Aura's value is not (string): " + base.Value1 + ", " + base.Value2;
	}
}
