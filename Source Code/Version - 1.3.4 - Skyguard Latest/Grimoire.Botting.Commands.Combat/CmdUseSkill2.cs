using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Combat;

public class CmdUseSkill2 : IBotCommand
{
	public string Monster { get; set; }

	public string Skill { get; set; }

	public string Index { get; set; }

	public int SafeHp { get; set; }

	public int SafeMp { get; set; }

	public bool Wait { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		string monster = instance.Value(Monster);
		if (!BotUtilities.ShouldUseSkill && monster != "Self-targeted")
		{
			return;
		}
		BotData.BotState = BotData.State.Combat;
		bool onSafeHp = SafeHp != 100;
		bool onSafeMp = SafeMp != 100;
		bool use = (onSafeHp ? ((double)Player.Health / (double)Player.HealthMax * 100.0 <= (double)SafeHp) : (onSafeMp && (double)Player.Mana / (double)Player.ManaMax * 100.0 <= (double)SafeMp));
		if (Wait)
		{
			await Task.Delay(Player.SkillAvailable(Index));
		}
		if (!(onSafeHp || onSafeMp) || use)
		{
			if (Index != "5" || monster != "Self-targeted")
			{
				Player.AttackMonster(World.IsMonsterAvailable(monster) ? monster : "*");
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
