using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Quest;

public class CmdQuestlist : IBotCommand
{
	public enum State
	{
		Add,
		Remove,
		Clear
	}

	public string QuestID { get; set; }

	public string ItemID { get; set; }

	public State state { get; set; }

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
		switch (state)
		{
		case State.Add:
			if (Player.Quests.LoadedQuests.Find((Grimoire.Game.Data.Quest q) => q.Id == _questID) == null)
			{
				Player.Quests.GetQuest(_questID);
				await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find((Grimoire.Game.Data.Quest q) => q.Id == _questID) != null, null, 6, 500);
			}
			BotManager.Instance.AddQuest(_questID, _itemID, toConfig: true);
			break;
		case State.Remove:
			if (Player.Quests.LoadedQuests.Find((Grimoire.Game.Data.Quest q) => q.Id == _questID) == null)
			{
				Player.Quests.GetQuest(_questID);
				await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find((Grimoire.Game.Data.Quest q) => q.Id == _questID) != null, null, 6, 500);
			}
			BotManager.Instance.RemoveQuest(_questID, _itemID, toConfig: true);
			break;
		case State.Clear:
			BotManager.Instance.lstQuests.Items.Clear();
			instance.Configuration.Quests.Clear();
			break;
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return state switch
		{
			State.Add => "Add to Quest list: " + QuestID + ", " + ((ItemID != null) ? ItemID : null), 
			State.Remove => "Remove from Quest list: " + QuestID + ", " + ((ItemID != null) ? ItemID : null), 
			State.Clear => "Clear Quest list", 
			_ => "Quest list", 
		};
	}
}
