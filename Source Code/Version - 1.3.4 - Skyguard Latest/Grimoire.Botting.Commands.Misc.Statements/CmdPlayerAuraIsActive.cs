using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayerAuraIsActive : StatementCommand, IBotCommand
{
	public CmdPlayerAuraIsActive()
	{
		base.Tag = "Player Aura";
		base.Text = "Player Aura is active";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Player.isAuraActive("Self", instance.Value(base.Value1)))
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
		return "Player Aura is active: " + base.Value1;
	}
}
