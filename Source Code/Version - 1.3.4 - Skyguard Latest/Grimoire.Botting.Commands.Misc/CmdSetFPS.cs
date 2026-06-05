using System.Threading.Tasks;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc;

public class CmdSetFPS : IBotCommand
{
	public int FPS { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		Flash.Call("SetFPS", FPS);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return $"Set FPS: {FPS}";
	}
}
