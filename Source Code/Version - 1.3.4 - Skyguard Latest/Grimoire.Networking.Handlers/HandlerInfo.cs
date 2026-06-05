using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers;

public class HandlerInfo : IJsonMessageHandler
{
	public string[] HandledCommands { get; } = new string[4] { "ct", "cb", "moveToArea", "uotls" };

	public async void Handle(JsonMessage message)
	{
		await ParseInfo(message, message.Command);
	}

	void IJsonMessageHandler.Handle(JsonMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}

	public async Task ParseInfo(JsonMessage message, string command)
	{
		switch (command)
		{
		case "cb":
			return;
		case "moveToArea":
			return;
		}
		_ = command == "uotls";
	}
}
