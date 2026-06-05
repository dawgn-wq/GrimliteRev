using System;
using System.Collections.Generic;
using System.Linq;
using Grimoire.Tools;

namespace Grimoire.Game.Data;

public class Quests
{
	public List<Quest> QuestTree => Flash.Call<List<Quest>>("GetQuestTree", new string[0]);

	public List<Quest> AcceptedQuests => QuestTree.Where((Quest q) => q.IsInProgress).ToList();

	public List<Quest> UnacceptedQuests => QuestTree.Where((Quest q) => !q.IsInProgress).ToList();

	public List<Quest> CompletedQuests => QuestTree.Where((Quest q) => q.CanComplete).ToList();

	public List<Quest> LoadedQuests { get; set; } = new List<Quest>();

	public event Action<List<Quest>> QuestsLoaded;

	public event Action<CompletedQuest> QuestCompleted;

	public void OnQuestsLoaded(List<Quest> quests)
	{
		this.QuestsLoaded?.Invoke(quests);
	}

	public void OnQuestCompleted(CompletedQuest quest)
	{
		this.QuestCompleted?.Invoke(quest);
	}

	public void Accept(int questId)
	{
		Flash.Call("Accept", questId.ToString());
	}

	public void Accept(string questId)
	{
		Flash.Call("Accept", questId);
	}

	public void Complete(int questId)
	{
		Flash.Call("Complete", questId.ToString());
	}

	public void Complete(string questId)
	{
		Flash.Call("Complete", questId);
	}

	public void Complete(string questId, string itemId)
	{
		Flash.Call("Complete", itemId, bool.TrueString);
	}

	public void Load(int id)
	{
		Flash.Call("LoadQuest", id.ToString());
	}

	public void Load(List<int> ids)
	{
		Flash.Call("LoadQuests", string.Join(",", ids));
	}

	public void Get(List<int> ids)
	{
		Flash.Call("GetQuests", string.Join(",", ids.Select((int i) => i.ToString())));
	}

	public void GetQuest(int id)
	{
		Flash.Call("GetQuests", id);
	}

	public bool IsInProgress(int id)
	{
		return Flash.Call<bool>("IsInProgress", new string[1] { id.ToString() });
	}

	public bool IsInProgress(string id)
	{
		return Flash.Call<bool>("IsInProgress", new string[1] { id });
	}

	public bool CanComplete(int id)
	{
		return Flash.Call<bool>("CanComplete", new string[1] { id.ToString() });
	}

	public bool CanComplete(string id)
	{
		return Flash.Call<bool>("CanComplete", new string[1] { id });
	}

	public bool IsAvailable(int id)
	{
		return Flash.Call<bool>("IsAvailable", new string[1] { id.ToString() });
	}
}
