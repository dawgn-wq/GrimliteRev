using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdDelay : IBotCommand
{
	public int Delay { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		await Task.Delay(Delay);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Delay: " + Delay;
	}
}
