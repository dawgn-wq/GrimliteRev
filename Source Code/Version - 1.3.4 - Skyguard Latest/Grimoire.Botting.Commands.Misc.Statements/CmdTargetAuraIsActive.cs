using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdTargetAuraIsActive : StatementCommand, IBotCommand
{
	public CmdTargetAuraIsActive()
	{
		base.Tag = "Target Aura";
		base.Text = "Target Aura is active";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Player.isAuraActive("Target", instance.Value(base.Value1)))
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
		return "Target Aura is active: " + base.Value1;
	}
}
