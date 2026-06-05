using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdSetClientLevel : IBotCommand
{
	public async Task Execute(IBotEngine instance)
	{
		OptionsManager.SetClientLevel();
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Set client level to max";
	}
}
