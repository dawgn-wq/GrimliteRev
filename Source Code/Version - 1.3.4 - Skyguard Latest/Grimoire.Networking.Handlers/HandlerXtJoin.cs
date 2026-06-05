using Grimoire.Botting;
using Grimoire.Tools;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers;

public class HandlerXtJoin : IXtMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "server" };

	public void Handle(XtMessage message)
	{
		if (message.RawContent.Contains("You joined "))
		{
			if (OptionsManager.ChangeChat)
			{
				Flash.Call("ChangeName", "You");
			}
			if (BotManager.Instance.CustomName != null)
			{
				BotManager.Instance.CustomName = BotManager.Instance.CustomName;
			}
			if (BotManager.Instance.CustomGuild != null)
			{
				BotManager.Instance.CustomGuild = BotManager.Instance.CustomGuild;
			}
		}
	}

	void IXtMessageHandler.Handle(XtMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
