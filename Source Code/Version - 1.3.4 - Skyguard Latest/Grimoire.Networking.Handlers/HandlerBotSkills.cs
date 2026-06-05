using System;
using System.Linq;
using Grimoire.Game;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers;

public class HandlerBotSkills : IJsonMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "ct" };

	public async void Handle(JsonMessage message)
	{
		if (message.RawContent.IndexOf("\"msg\":", StringComparison.OrdinalIgnoreCase) > -1 && message.RawContent.IndexOf("counter attack", StringComparison.OrdinalIgnoreCase) > -1 && message.RawContent.Contains("\"strFrame\":\"" + Player.Cell + "\""))
		{
			Proxy.DisableBotSkill();
		}
		else if (message.RawContent.IndexOf("\"nam\":\"Counter Attack\"", StringComparison.OrdinalIgnoreCase) != -1)
		{
			JArray jArray = (JArray)(message.DataObject?["a"]);
			if (jArray != null && jArray.Any((JToken a) => (a["cmd"].ToString() == "aura-" || a["cmd"].ToString() == "aura--") && a["aura"]["nam"].ToString().Equals("Counter Attack", StringComparison.OrdinalIgnoreCase)))
			{
				Proxy.EnableBotSkill();
			}
		}
	}

	void IJsonMessageHandler.Handle(JsonMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
