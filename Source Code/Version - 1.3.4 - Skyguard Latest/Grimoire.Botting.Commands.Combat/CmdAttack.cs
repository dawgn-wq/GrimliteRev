using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Combat;

public class CmdAttack : IBotCommand
{
	public string Monster { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		Player.AttackMonster(instance.Value(Monster));
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Attack: " + Monster;
	}
}
