using System.Threading;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using Newtonsoft.Json;

namespace Grimoire.Networking.Handlers;

public class HandlerArea : IJsonMessageHandler
{
	private static CancellationTokenSource _cts = new CancellationTokenSource();

	public string[] HandledCommands { get; } = new string[1] { "moveToArea" };

	public async void Handle(JsonMessage message)
	{
		await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.ActiveCutscene == 0 && !World.mapLoadInProgress && Player.IsLoggedIn, () => !_cts.IsCancellationRequested, -1, 500, 500);
		Map currentMap = JsonConvert.DeserializeObject<Map>(message.DataObject.ToString());
		World.CurrentMap = currentMap;
	}

	void IJsonMessageHandler.Handle(JsonMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
