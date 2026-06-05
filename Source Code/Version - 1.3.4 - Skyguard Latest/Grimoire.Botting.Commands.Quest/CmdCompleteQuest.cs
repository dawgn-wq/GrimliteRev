using System;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Quest;

public class CmdCompleteQuest : IBotCommand
{
	public Grimoire.Game.Data.Quest Quest { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		await instance.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete), null, 6, 500);
		if (!Player.Quests.CanComplete(Quest.Id))
		{
			LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Failed to complete the quest: \"{Quest.Name}\" - ID {Quest.Id}.\r\n");
			return;
		}
		BotData.BotState = BotData.State.Quest;
		_ = Player.Cell;
		_ = Player.Pad;
		if (instance.Configuration.ExitCombatBeforeQuest)
		{
			while (instance.IsRunning && Player.CurrentState == Player.State.InCombat)
			{
				BotData.BotState = BotData.State.Quest;
				Player.MoveToCell(Player.Cell, Player.Pad);
				await Task.Delay(2500);
			}
		}
		if (Player.CurrentState == Player.State.InCombat)
		{
			await Task.Delay(2500);
		}
		Quest.Complete();
		await instance.WaitUntil(() => !Player.Quests.IsInProgress(Quest.Id), null, 6, 500);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Complete quest: " + ((Quest.ItemId != null && Quest.ItemId != "0") ? $"{Quest.Id}, {Quest.ItemId}" : Quest.Id.ToString());
	}
}
