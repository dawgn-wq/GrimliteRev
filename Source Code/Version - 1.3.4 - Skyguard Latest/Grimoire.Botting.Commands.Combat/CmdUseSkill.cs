using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Combat;

public class CmdUseSkill : IBotCommand
{
	public string Skill { get; set; }

	public string Index { get; set; }

	public int SafeHp { get; set; }

	public int SafeMp { get; set; }

	public bool Wait { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Combat;
		if (Wait)
		{
			await Task.Delay(Player.SkillAvailable(Index));
		}
		if ((double)Player.Health / (double)Player.HealthMax * 100.0 <= (double)SafeHp && (double)Player.Mana / (double)Player.ManaMax * 100.0 <= (double)SafeMp)
		{
			if (Index != "5")
			{
				Player.AttackMonster("*");
			}
			Player.UseSkill(Index);
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Skill " + Skill;
	}
}
