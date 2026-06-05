using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdRestart : IBotCommand
{
	public async Task Execute(IBotEngine instance)
	{
		instance.Index = 0;
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Restart bot";
	}
}
