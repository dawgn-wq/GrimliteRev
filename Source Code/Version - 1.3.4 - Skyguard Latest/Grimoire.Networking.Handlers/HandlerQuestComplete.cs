using System;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers;

public class HandlerQuestComplete : IJsonMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "ccqr" };

	public void Handle(JsonMessage message)
	{
		if (message.DataObject["bSuccess"].Value<int>() == 1)
		{
			if (Bot.Instance.questFailureCounter.IsRunning)
			{
				Bot.Instance.questFailureCounter.Stop();
			}
			CompletedQuest quest = message.DataObject.ToObject<CompletedQuest>();
			Player.Quests.OnQuestCompleted(quest);
			LogForm.Instance.AppendDebug(string.Format("[{0:hh:mm:ss}] Quest completed: \"{1}\" - ID {2}.\r\n", DateTime.Now, message.DataObject["sName"].Value<string>(), message.DataObject["QuestID"].Value<int>()));
		}
		else if (BotManager.Instance.chkReloginOnQuestFailure.Checked && BotManager.Instance.ActiveBotEngine.IsRunning)
		{
			if (!Bot.Instance.questFailureCounter.IsRunning)
			{
				Bot.Instance.questFailureCounter.Start();
			}
			if (Bot.Instance.questFailureCounter.ElapsedMilliseconds > 15000)
			{
				Bot.Instance.questFailureCounter.Stop();
				return;
			}
			Bot.Instance.questCompletionFailure++;
			Bot.Instance.ReloginOnQuestFailure();
		}
	}

	void IJsonMessageHandler.Handle(JsonMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
