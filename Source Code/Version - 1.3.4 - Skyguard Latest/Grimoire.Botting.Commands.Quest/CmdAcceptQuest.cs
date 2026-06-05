using System.Linq;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Quest;

public class CmdAcceptQuest : IBotCommand
{
	public Grimoire.Game.Data.Quest Quest { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Quest;
		await instance.WaitUntil(() => Player.Quests.QuestTree.Any((Grimoire.Game.Data.Quest q) => q.Id == Quest.Id), null, 6, 500);
		await instance.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), null, 6, 500);
		Quest.Accept();
		await instance.WaitUntil(() => Player.Quests.IsInProgress(Quest.Id), null, 6, 500);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return $"Accept quest: {Quest.Id}";
	}
}
