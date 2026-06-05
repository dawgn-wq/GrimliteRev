using System.Threading.Tasks;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Misc;

public class CmdToggleProvokeInMap : IBotCommand
{
	public int Type { get; set; }

	public string ProvokePacket { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		switch (Type)
		{
		case 0:
			OptionsManager.ProvokeAllMonster = false;
			Root.Instance.provokeAllMonsterInMapToolStripMenuItem.Checked = false;
			BotManager.Instance.chkProvokeAllMon.Checked = false;
			await BotUtilities.MoveOutOfCombat();
			break;
		case 1:
			OptionsManager.ProvokeAllMonster = true;
			Root.Instance.provokeAllMonsterInMapToolStripMenuItem.Checked = true;
			BotManager.Instance.chkProvokeAllMon.Checked = true;
			break;
		default:
			OptionsManager.ProvokeAllMonster = !OptionsManager.ProvokeAllMonster;
			break;
		}
		OptionsManager.ProvokePacket = ProvokePacket;
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return Type switch
		{
			0 => "Provoke in map: Off", 
			1 => "Provoke in map: On", 
			_ => "Provoke in map: Toggle", 
		};
	}
}
