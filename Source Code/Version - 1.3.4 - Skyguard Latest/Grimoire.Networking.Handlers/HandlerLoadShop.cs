using Grimoire.Game;
using Grimoire.Game.Data;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers;

public class HandlerLoadShop : IJsonMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "loadShop" };

	public async void Handle(JsonMessage message)
	{
		JToken jToken = message.DataObject["shopinfo"];
		if (jToken != null)
		{
			World.OnShopLoaded(jToken.ToObject<ShopInfo>());
		}
	}

	void IJsonMessageHandler.Handle(JsonMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
