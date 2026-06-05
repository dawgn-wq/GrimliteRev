using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdBankList : IBotCommand
{
	public async Task Execute(IBotEngine instance)
	{
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Bank Item list";
	}
}
