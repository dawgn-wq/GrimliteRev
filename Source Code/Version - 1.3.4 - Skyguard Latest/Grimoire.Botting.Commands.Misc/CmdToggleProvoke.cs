using System.Threading.Tasks;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Misc;

public class CmdToggleProvoke : IBotCommand
{
	public int Type { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		switch (Type)
		{
		case 0:
			OptionsManager.ProvokeMonsters = false;
			Root.Instance.provokeToolStripMenuItem1.Checked = false;
			BotManager.Instance.chkProvoke.Checked = false;
			break;
		case 1:
			OptionsManager.ProvokeMonsters = true;
			Root.Instance.provokeToolStripMenuItem1.Checked = true;
			BotManager.Instance.chkProvoke.Checked = true;
			break;
		default:
			OptionsManager.ProvokeMonsters = !OptionsManager.ProvokeMonsters;
			break;
		}
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
			0 => "Provoke in cell: Off", 
			1 => "Provoke in cell: On", 
			_ => "Provoke in cell: Toggle", 
		};
	}
}
