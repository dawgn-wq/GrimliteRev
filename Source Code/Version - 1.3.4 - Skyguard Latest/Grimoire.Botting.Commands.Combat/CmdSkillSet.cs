using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Combat;

public class CmdSkillSet : IBotCommand
{
	public string Name { get; set; }

	public Task Execute(IBotEngine instance)
	{
		BotData.BotSkill = instance.Value(Name).ToUpper();
		return Task.FromResult<object>(null);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Use skill set: " + Name;
	}
}
