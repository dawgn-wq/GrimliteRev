using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Quest;

public class CmdAcceptQuest2 : IBotCommand
{
	public string QuestID { get; set; }

	private int _questID => int.Parse(BotManager.Instance.ActiveBotEngine.Value(QuestID));

	public async Task Execute(IBotEngine instance)
	{
		if (Player.Quests.LoadedQuests.Find((Grimoire.Game.Data.Quest q) => q.Id == _questID) == null)
		{
			Player.Quests.GetQuest(_questID);
			await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find((Grimoire.Game.Data.Quest q) => q.Id == _questID) != null, null, 6, 500);
		}
		BotData.BotState = BotData.State.Quest;
		await instance.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), null, 10, 500);
		Player.AcceptQuest(_questID);
		await instance.WaitUntil(() => Player.Quests.IsInProgress(_questID), null, 6, 500);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Accept quest: " + QuestID;
	}
}
