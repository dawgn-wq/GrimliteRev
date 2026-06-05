using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Botting;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;

namespace Grimoire.Game;

public class DropStack : IReadOnlyList<InventoryItem>, IReadOnlyCollection<InventoryItem>, IEnumerable<InventoryItem>, IEnumerable
{
	private readonly List<InventoryItem> _drops = new List<InventoryItem>();

	private readonly List<KeyValuePair<int, Stopwatch>> _cooldown = new List<KeyValuePair<int, Stopwatch>>();

	public List<string> MarkedDrops { get; set; } = new List<string>();

	public bool InProcess { get; set; }

	public int Count => _drops.Count;

	public InventoryItem this[int index]
	{
		get
		{
			if (index >= _drops.Count)
			{
				return null;
			}
			return _drops[index];
		}
	}

	public DropStack()
	{
		World.ItemDropped += OnItemDropped;
	}

	public IEnumerator<InventoryItem> GetEnumerator()
	{
		return _drops.GetEnumerator();
	}

	IEnumerator<InventoryItem> IEnumerable<InventoryItem>.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
		return this.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private void OnItemDropped(InventoryItem item)
	{
		if (_drops.All((InventoryItem d) => d.Id != item.Id))
		{
			_drops.Add(item);
		}
	}

	public async Task<bool> GetDrop(string itemName)
	{
		InventoryItem ınventoryItem = _drops.FirstOrDefault((InventoryItem d) => d.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
		bool flag = ınventoryItem != null;
		if (flag)
		{
			flag = await GetDrop(ınventoryItem.Id);
		}
		return flag;
	}

	public async Task<bool> RemoveAll(int itemId)
	{
		if (Contains(itemId))
		{
			_drops.RemoveAll((InventoryItem d) => d.Id == itemId);
			return true;
		}
		return false;
	}

	public async Task<bool> GetDrop(int itemId)
	{
		if (Contains(itemId))
		{
			InventoryItem drop = _drops.FirstOrDefault((InventoryItem d) => d.Id == itemId);
			InProcess = true;
			Player.AcceptDrop(itemId);
			await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !World.DropStack.Contains(drop.Name), null, 6, 500);
			InProcess = false;
			return true;
		}
		return false;
	}

	public bool Contains(int itemId)
	{
		return _drops.FirstOrDefault((InventoryItem d) => d.Id == itemId) != null;
	}

	public bool Contains(string itemName)
	{
		if (_drops.FirstOrDefault((InventoryItem d) => d.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)) == null)
		{
			return Bot.Instance.HasDropInInventory(itemName);
		}
		return true;
	}

	public async Task RejectDrops()
	{
		while (BotManager.Instance.ActiveBotEngine.IsRunning && BotManager.Instance.ActiveBotEngine.Configuration.EnableRejection)
		{
			Configuration configuration = BotManager.Instance.ActiveBotEngine.Configuration;
			string text;
			if (configuration.Drops.Count > 0)
			{
				string[] value = configuration.Drops.ToArray();
				text = string.Join(",", value).ToLower();
			}
			else
			{
				text = "";
			}
			Flash.Call("RejectDrop", text);
			await Task.Delay(100);
		}
	}
}
