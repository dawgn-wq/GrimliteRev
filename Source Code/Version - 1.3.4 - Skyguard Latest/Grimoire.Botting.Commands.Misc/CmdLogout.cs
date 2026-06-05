using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc;

public class CmdLogout : IBotCommand
{
	public async Task Execute(IBotEngine instance)
	{
		Player.Logout();
		await instance.WaitUntil(() => !Player.IsLoggedIn, null, 10, 500);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Logout";
	}
}
