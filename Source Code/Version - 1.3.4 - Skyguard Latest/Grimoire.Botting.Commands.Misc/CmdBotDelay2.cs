using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdBotDelay2 : IBotCommand
{
	public string Delay { get; set; }

	public Task Execute(IBotEngine instance)
	{
		instance.Configuration.BotDelay = int.Parse(instance.Value(Delay));
		return Task.FromResult<object>(null);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Set bot delay: " + Delay;
	}
}
