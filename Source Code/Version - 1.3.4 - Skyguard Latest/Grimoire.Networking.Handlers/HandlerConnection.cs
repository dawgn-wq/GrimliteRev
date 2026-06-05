using Grimoire.Game;

namespace Grimoire.Networking.Handlers;

public class HandlerConnection : IXtMessageHandler
{
	public string[] HandledCommands { get; } = new string[2] { "loginResponse", "logoutWarning" };

	public void Handle(XtMessage message)
	{
		string command = message.Command;
		if (!(command == "loginResponse"))
		{
			if (command == "logoutWarning")
			{
				Player.IsLoggedIn = false;
			}
		}
		else
		{
			Player.IsLoggedIn = true;
		}
	}

	void IXtMessageHandler.Handle(XtMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
