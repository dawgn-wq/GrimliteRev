using System;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers;

public class HandlerWarningsXt : IXtMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "logoutWarning" };

	public void Handle(XtMessage message)
	{
		LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] Logged out from the game due to a warning trigger.\r\n");
	}

	void IXtMessageHandler.Handle(XtMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
