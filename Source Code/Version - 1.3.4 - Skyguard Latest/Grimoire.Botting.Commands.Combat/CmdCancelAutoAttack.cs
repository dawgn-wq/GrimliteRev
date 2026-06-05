using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Combat;

public class CmdCancelAutoAttack : IBotCommand
{
	public async Task Execute(IBotEngine instance)
	{
		Player.CancelAutoAttack();
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Cancel auto attack";
	}
}
