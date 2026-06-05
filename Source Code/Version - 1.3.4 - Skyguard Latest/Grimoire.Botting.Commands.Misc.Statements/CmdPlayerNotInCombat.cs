using System.Threading.Tasks;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayerNotInCombat : StatementCommand, IBotCommand
{
	public CmdPlayerNotInCombat()
	{
		base.Tag = "Player";
		base.Text = "Player is not in combat";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Flash.Call<bool>("CheckPlayerInCombat", new string[1] { instance.Value(base.Value1) }))
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
		return "Player is not in combat state: " + base.Value1;
	}
}
