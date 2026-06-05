using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdSkillIsAvailable : StatementCommand, IBotCommand
{
	public CmdSkillIsAvailable()
	{
		base.Tag = "This player";
		base.Text = "Skill is available";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Player.IsSkillAvailable(instance.Value(base.Value1)))
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
		return "Skill is available:" + base.Value1;
	}
}
