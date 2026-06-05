using System.Threading.Tasks;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Misc;

public class CmdStop : IBotCommand
{
	public async Task Execute(IBotEngine instance)
	{
		BotManager.Instance.btnBotStart.Enabled = false;
		Root.Instance.stopToolStripMenuItem.Enabled = false;
		BotManager.Instance.ActiveBotEngine.Stop();
		BotManager.Instance.CustomCommandToggle(Type: true);
		BotManager.Instance.SelectionModeToggle(Type: false);
		BotManager.Instance.BotStateChanged(IsRunning: false);
		await Task.Delay(2000);
		Root.Instance.BotStateChanged(IsRunning: false);
		BotManager.Instance.btnBotStart.Enabled = true;
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Stop bot";
	}
}
