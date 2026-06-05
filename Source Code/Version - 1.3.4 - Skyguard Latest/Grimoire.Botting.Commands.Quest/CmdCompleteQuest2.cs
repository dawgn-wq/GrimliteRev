using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Quest;

public class CmdCompleteQuest2 : IBotCommand
{
	public string QuestID { get; set; }

	public string ItemID { get; set; }

	private int _questID => int.Parse(BotManager.Instance.ActiveBotEngine.Value(QuestID));

	private string _itemID
	{
		get
		{
			if (string.IsNullOrEmpty(ItemID))
			{
				return null;
			}
			return BotManager.Instance.ActiveBotEngine.Value(ItemID);
		}
	}

	public async Task Execute(IBotEngine instance)
	{
		if (Player.Quests.LoadedQuests.Find((Grimoire.Game.Data.Quest q) => q.Id == _questID) == null)
		{
			Player.Quests.GetQuest(_questID);
			await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find((Grimoire.Game.Data.Quest q) => q.Id == _questID) != null, null, 6, 500);
		}
		if (!Player.Quests.CanComplete(_questID))
		{
			Grimoire.Game.Data.Quest quest = Player.Quests.LoadedQuests.FirstOrDefault((Grimoire.Game.Data.Quest lQuest) => lQuest.Id == _questID);
			LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Failed to complete the quest: \"{quest.Name}\" - ID {quest.Id}.\r\n");
			return;
		}
		BotData.BotState = BotData.State.Quest;
		await instance.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete), null, 10, 500);
		if (instance.Configuration.ExitCombatBeforeQuest)
		{
			await BotUtilities.MoveOutOfCombat();
		}
		Player.CompleteQuest(_questID, _itemID);
		await instance.WaitUntil(() => !Player.Quests.IsInProgress(_questID), null, 6, 500);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Complete quest: " + ((ItemID != null && ItemID != "0") ? (QuestID + ", " + ItemID) : QuestID);
	}
}
