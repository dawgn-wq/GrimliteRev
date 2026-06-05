using System.Threading.Tasks;
using Grimoire.Botting;
using Grimoire.Tools;

namespace Grimoire.Networking.Handlers;

public class HandlerSkills : IJsonMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "sAct" };

	public async void Handle(JsonMessage message)
	{
		await Task.Delay(10);
		Flash.Call("getRange");
		OptionsManager.SetInfiniteRange(OptionsManager.InfiniteRange);
	}

	void IJsonMessageHandler.Handle(JsonMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
