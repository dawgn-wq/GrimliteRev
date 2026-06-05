using System.Threading.Tasks;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers;

public class HandlerAFK : IXtMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "afk" };

	public async void Handle(XtMessage message)
	{
		string text = message.Arguments[5];
		if (text == "true")
		{
			if (BotManager.Instance.ActiveBotEngine.IsRunning && BotManager.Instance.ActiveBotEngine.Configuration.AutoRelogin && BotManager.Instance.ActiveBotEngine.Configuration.AFK)
			{
				Player.Logout();
			}
			if (OptionsManager._antiAfk)
			{
				await Task.Delay(750);
				Proxy.Instance.SendToServer("%xt%zm%afk%1%false%");
			}
		}
	}

	void IXtMessageHandler.Handle(XtMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
