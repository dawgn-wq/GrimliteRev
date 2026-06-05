using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdClearTemp : IBotCommand
{
	public async Task Execute(IBotEngine instance)
	{
		Configuration.Tempvalues.Clear();
		Configuration.Tempvariable.Clear();
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Clear Variables and Integers";
	}
}
