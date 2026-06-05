using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AxShockwaveFlashObjects;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;

namespace Grimoire.Botting;

public static class BotUtilities
{
	public static AxShockwaveFlash flash;

	public static bool ActiveBot { get; set; }

	public static bool BusyClient { get; set; }

	public static bool HasLoadedQuests { get; set; }

	public static bool ShouldUseSkill { get; set; }

	public static async Task WaitUntil(this IBotEngine instance, Func<bool> condition, Func<bool> prerequisite = null, int timeout = 15, int delay = 1000, int finalDelay = 0)
	{
		int iterations = 0;
		while ((prerequisite ?? ((Func<bool>)(() => instance.IsRunning && Player.IsLoggedIn && Player.IsAlive)))() && !condition() && (iterations < timeout || timeout == -1))
		{
			await Task.Delay(delay);
			iterations++;
		}
		await Task.Delay(finalDelay);
	}

	public static bool RequiresDelay(this IBotCommand cmd)
	{
		if (cmd is StatementCommand || cmd is CmdIndex || cmd is CmdLabel || cmd is CmdGotoLabel || cmd is CmdBlank || cmd is CmdSkillSet)
		{
			return false;
		}
		return true;
	}

	public static bool ShouldLog(this IBotCommand cmd)
	{
		if (cmd is CmdBlank || cmd is CmdBlank2 || cmd is CmdBlank3)
		{
			return false;
		}
		return true;
	}

	public static string LogSpecificCmd(this IBotCommand cmd)
	{
		if (cmd is CmdLabel)
		{
			return "On label ";
		}
		if (cmd is StatementCommand && !cmd.ToString().StartsWith("Set") && !cmd.ToString().StartsWith("Get") && !cmd.ToString().StartsWith("Update"))
		{
			return "On statement - ";
		}
		return null;
	}

	public static async Task GetBotQuests(this IBotEngine instance)
	{
		BotData.BotQuests = new List<int>();
		foreach (IBotCommand command in instance.Configuration.Commands)
		{
			if (command.GetType().ToString().Contains("Quest"))
			{
				PropertyInfo propertyInfo = command.GetType().GetProperty("Quest") ?? command.GetType().GetProperty("QuestID") ?? command.GetType().GetProperty("Value1");
				if (!propertyInfo.GetValue(command).ToString().StartsWith("[") && !string.IsNullOrWhiteSpace(propertyInfo.GetValue(command).ToString()))
				{
					string s = instance.Value(propertyInfo.GetValue(command).ToString());
					BotData.BotQuests.Add(int.Parse(s));
				}
			}
		}
		BotData.BotQuests.AddRange(instance.Configuration.Quests.ConvertAll((Quest q) => q.Id));
		BotData.BotQuests = (BotData.BotQuests?.GroupBy((int id) => id)).Select((IGrouping<int, int> x) => x.First()).ToList();
	}

	public static async Task LoadBotQuests(this IBotEngine instance)
	{
		if (HasLoadedQuests)
		{
			return;
		}
		await instance.GetBotQuests();
		List<List<int>> botQuestsList = (from x in BotData.BotQuests.Select((int x, int i) => new
			{
				Index = i,
				Value = x
			})
			group x by x.Index / 30 into x
			select x.Select(v => v.Value).ToList()).ToList();
		List<int> list = BotData.BotQuests?.Where((int num) => Player.Quests.LoadedQuests.Any((Quest q) => q.Id == num) && instance.Configuration.Quests.Any((Quest q) => q.Id == num && !q.IsInProgress)).ToList();
		if (list.Count > 0)
		{
			foreach (int id in list)
			{
				await instance.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), () => instance.IsRunning, 10, 500);
				Player.Quests.Accept(id);
			}
		}
		foreach (List<int> botQuests in botQuestsList)
		{
			if (botQuestsList.Count > 1)
			{
				await Task.Delay(1500);
			}
			Player.Quests.Get(botQuests);
			await instance.WaitUntil(() => Player.Quests.LoadedQuests.Any((Quest q) => q.Id == botQuests[0]), () => instance.IsRunning, 10, 500);
		}
		HasLoadedQuests = true;
	}

	public static void AddConfigSkills(Configuration config)
	{
		if (config.Skills.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < config.Skills.Count; i++)
		{
			if (config.Skills[i].Type == Skill.SkillType.Label)
			{
				BotData.SkillSet.Add(config.Skills[i].Text.ToUpper(), i);
			}
		}
	}

	public static void MoveToSelfCell()
	{
		Player.MoveToCell(Player.Cell, Player.Pad);
	}

	public static async void MoveToBotCell()
	{
		if (BotData.BotCell != null && !Player.Cell.Equals(BotData.BotCell, StringComparison.OrdinalIgnoreCase))
		{
			Player.MoveToCell(BotData.BotCell, BotData.BotPad);
		}
	}

	public static async Task MoveOutOfCombat()
	{
		if (Player.CurrentState == Player.State.InCombat)
		{
			Player.MoveToCell(Player.Cell, Player.Pad);
			await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.CurrentState != Player.State.InCombat, null, 10, 500);
		}
	}
}
