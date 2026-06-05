using System.Collections.Generic;
using System.Linq;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Networking.Handlers;

public class HandlerGetQuests : IJsonMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "getQuests" };

	public void Handle(JsonMessage message)
	{
		Dictionary<int, Quest> dictionary = message.DataObject?["quests"]?.ToObject<Dictionary<int, Quest>>();
		if (dictionary != null && dictionary.Count > 0)
		{
			Player.Quests.OnQuestsLoaded(dictionary.Select((KeyValuePair<int, Quest> q) => q.Value).ToList());
			Player.Quests.LoadedQuests.AddRange(dictionary.Select((KeyValuePair<int, Quest> q) => q.Value).ToList());
		}
	}

	void IJsonMessageHandler.Handle(JsonMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
