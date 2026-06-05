using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdSkillIsNotAvailable : StatementCommand, IBotCommand
{
	public CmdSkillIsNotAvailable()
	{
		base.Tag = "This player";
		base.Text = "Skill is not available";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.IsSkillAvailable(instance.Value(base.Value1)))
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
		return "Skill is not available:" + base.Value1;
	}
}
