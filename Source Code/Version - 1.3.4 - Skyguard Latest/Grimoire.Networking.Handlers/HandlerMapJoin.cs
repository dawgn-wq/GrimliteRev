using System.Threading.Tasks;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Tools;

namespace Grimoire.Networking.Handlers;

public class HandlerMapJoin : IJsonMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "moveToArea" };

	public void Handle(JsonMessage message)
	{
		OptionsManager.RoomNumber = World.RoomNumber;
		if (OptionsManager.HideRoom)
		{
			HideRoom();
		}
	}

	void IJsonMessageHandler.Handle(JsonMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}

	public async void HideRoom()
	{
		await Task.Delay(10);
		Flash.Call("ChangeAreaName", BotClientConfig.Instance.GetValue<string>("areaName") ?? "discord.io/aqwbots");
	}
}
