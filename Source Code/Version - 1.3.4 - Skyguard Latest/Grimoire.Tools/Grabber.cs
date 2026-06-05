using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using HtmlAgilityPack;

namespace Grimoire.Tools;

public static class Grabber
{
	public static void GrabQuests(TreeView tree)
	{
		List<Quest> list = Player.Quests.QuestTree?.OrderBy((Quest q) => q.Id).ToList();
		if (list == null || list.Count <= 0)
		{
			return;
		}
		foreach (Quest item in list)
		{
			try
			{
				TreeNode treeNode = tree.Nodes.Add($"{item.Id} - {item.Name}");
				treeNode.Nodes.Add($"ID: {item.Id}");
				treeNode.Nodes.Add("Description: " + item.Description + " ");
				treeNode.Nodes.Add("End Description: " + item.EndText + " ");
				treeNode.ContextMenuStrip = MenuQuest(item.Id);
				List<InventoryItem> requiredItems = item.RequiredItems;
				if (requiredItems != null && requiredItems.Count > 0)
				{
					TreeNode treeNode2 = treeNode.Nodes.Add("Required items");
					treeNode2.ContextMenuStrip = MenuItems(item.RequiredItems);
					foreach (InventoryItem requiredItem in item.RequiredItems)
					{
						TreeNode treeNode3 = treeNode2.Nodes.Add(requiredItem.Name);
						treeNode3.ContextMenuStrip = MenuItem(requiredItem);
						treeNode3.Nodes.Add($"ID: {requiredItem.Id}");
						treeNode3.Nodes.Add($"Quantity: {requiredItem.Quantity}");
						treeNode3.Nodes.Add("Temporary: " + (requiredItem.IsTemporary ? "Yes" : "No"));
						treeNode3.Nodes.Add("Description: " + requiredItem.Description);
					}
				}
				List<InventoryItem> rewards = item.Rewards;
				if (rewards == null || rewards.Count <= 0)
				{
					continue;
				}
				TreeNode treeNode4 = treeNode.Nodes.Add("Rewards");
				treeNode4.ContextMenuStrip = MenuItems(item.Rewards);
				foreach (InventoryItem reward in item.Rewards)
				{
					TreeNode treeNode5 = treeNode4.Nodes.Add(reward.Name);
					treeNode5.ContextMenuStrip = MenuItem(reward);
					treeNode5.Nodes.Add($"ID: {reward.Id}");
					treeNode5.Nodes.Add($"Quantity: {reward.Quantity}");
					treeNode5.Nodes.Add("Drop chance: " + (reward.DropChance.Contains("100") ? "Guaranteed" : (reward.DropChance + "%")));
					ItemBase ıtemBase = item.oRewards.Find((ItemBase x) => x.Name == reward.Name);
					treeNode5.Nodes.Add("Category: " + ıtemBase.Category);
					treeNode5.Nodes.Add("Description: " + ıtemBase.Description);
					if (!string.IsNullOrEmpty(ıtemBase.File))
					{
						treeNode5.ContextMenuStrip = MenuItem(ıtemBase);
						treeNode5.Nodes.Add("sFile: " + ıtemBase.File);
						treeNode5.Nodes.Add("sLink: " + ıtemBase.Link);
					}
				}
			}
			catch
			{
			}
		}
	}

	public static void GrabShopItems(TreeView tree)
	{
		List<ShopInfo> list = World.LoadedShops?.OrderBy((ShopInfo s) => s.Name).ToList();
		if (list == null || list.Count <= 0)
		{
			return;
		}
		foreach (ShopInfo item in list)
		{
			TreeNode treeNode = tree.Nodes.Add(item.Name);
			treeNode.ContextMenuStrip = Wiki(item);
			treeNode.Nodes.Add($"ID: {item.Id}");
			treeNode.Nodes.Add("Location: " + item.Location);
			List<InventoryItem> ıtems = item.Items;
			if (ıtems == null || ıtems.Count <= 0)
			{
				continue;
			}
			TreeNode treeNode2 = treeNode.Nodes.Add("Items");
			foreach (InventoryItem ıtem in item.Items)
			{
				TreeNode treeNode3 = treeNode2.Nodes.Add(ıtem.Name);
				treeNode3.ContextMenuStrip = Wiki(ıtem);
				treeNode3.Nodes.Add($"Shop item ID: {ıtem.ShopItemId}");
				treeNode3.Nodes.Add($"ID: {ıtem.Id}");
				treeNode3.Nodes.Add(string.Format("Cost: {0} {1}", ıtem.Cost, ıtem.IsAcItem ? "AC" : "Gold"));
				treeNode3.Nodes.Add("Category: " + ıtem.Category);
				treeNode3.Nodes.Add("Description: " + ıtem.Description);
				if (ıtem.IsEquippableNonItem || ıtem.IsWeapon)
				{
					treeNode3.Nodes.Add("sFile: " + ıtem.File);
					treeNode3.Nodes.Add("sLink: " + ıtem.Link);
				}
			}
		}
	}

	public static void GrabQuestIds(TreeView tree)
	{
		List<Quest> list = Player.Quests.QuestTree?.OrderBy((Quest q) => q.Id).ToList();
		if (list == null || list.Count <= 0)
		{
			return;
		}
		foreach (Quest item in list)
		{
			tree.Nodes.Add($"{item.Id} - {item.Name}").ContextMenuStrip = MenuQuest(item.Id);
		}
	}

	public static void GrabInventoryItems(TreeView tree)
	{
		GrabItems(tree, inventory: true);
	}

	public static void GrabBankItems(TreeView tree)
	{
		GrabItems(tree, inventory: false);
	}

	private static void GrabItems(TreeView tree, bool inventory)
	{
		List<InventoryItem> list = (inventory ? Player.Inventory.Items : Player.Bank.Items)?.OrderBy((InventoryItem i) => i.Name).ToList();
		if (list == null || list.Count <= 0)
		{
			return;
		}
		foreach (InventoryItem item in list)
		{
			TreeNode treeNode = tree.Nodes.Add(item.Name);
			treeNode.ContextMenuStrip = Wiki(item);
			treeNode.Nodes.Add($"ID: {item.Id}");
			treeNode.Nodes.Add($"Char Item ID: {item.CharItemId}");
			treeNode.Nodes.Add($"Quantity: {item.Quantity}/{item.MaxStack}");
			treeNode.Nodes.Add($"AC-tagged: {item.IsAcItem}");
			treeNode.Nodes.Add("Category: " + item.Category);
			treeNode.Nodes.Add("Description: " + item.Description);
			if (item.IsEquippableNonItem || item.IsWeapon)
			{
				treeNode.Nodes.Add("sFile: " + item.File);
				treeNode.Nodes.Add("sLink: " + item.Link);
			}
		}
	}

	public static void GrabTempItems(TreeView tree)
	{
		List<TempItem> list = Player.TempInventory.Items?.OrderBy((TempItem i) => i.Name).ToList();
		if (list == null || list.Count <= 0)
		{
			return;
		}
		foreach (TempItem item in list)
		{
			TreeNode treeNode = tree.Nodes.Add(item.Name);
			treeNode.ContextMenuStrip = Wiki(item.Name);
			treeNode.Nodes.Add($"ID: {item.Id}");
			treeNode.Nodes.Add($"Quantity: {item.Quantity}");
		}
	}

	public static void GrabMonsters(TreeView tree)
	{
		List<Monster> list = (World.MonstersInMap?.GroupBy((Monster m) => m.Name)).Select((IGrouping<string, Monster> x) => x.First()).ToList();
		if (list == null || list.Count <= 0)
		{
			return;
		}
		foreach (Monster item in list)
		{
			TreeNode treeNode = tree.Nodes.Add(item.Name);
			treeNode.ContextMenuStrip = Wiki(item.Name);
			treeNode.Nodes.Add($"ID: {item.Id}");
			treeNode.Nodes.Add("Race: " + item.Race);
			treeNode.Nodes.Add($"Level: {item.Level}");
			treeNode.Nodes.Add("Health: " + item.MaxHealth.ToString("N", new CultureInfo("en-US")).Split('.')[0]);
			TreeNode treeNode2 = treeNode.Nodes.Add("Drops");
			treeNode2.Nodes.Add("Loading..");
			treeNode2.TreeView.MouseDown += delegate(object S, MouseEventArgs E)
			{
				try
				{
					if (treeNode2.ForeColor != Color.Gray && !treeNode2.IsExpanded && treeNode2.FirstNode.Text == "Loading.." && treeNode.TreeView.GetNodeAt(E.Location).Text == "Drops")
					{
						Monster_Drops(treeNode2, item);
					}
				}
				catch
				{
				}
			};
		}
	}

	private static async void Monster_Drops(TreeNode treeNode2, Monster item)
	{
		treeNode2.ForeColor = Color.Gray;
		try
		{
			HtmlWeb web = new HtmlWeb();
			web.UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)";
            HtmlAgilityPack.HtmlDocument val = await web.LoadFromWebAsync("https://aqwwiki.wikidot.com/" + item.Name);
			HtmlNodeCollection targeted = val.DocumentNode.SelectNodes("//div[@id='page-content']");
			bool isTabbed = false;
			if (val.ParsedText.Contains("usually refers to"))
			{
				foreach (HtmlNode item2 in (IEnumerable<HtmlNode>)val.DocumentNode.SelectNodes("//div[@id='page-content']//p//a[@href]"))
				{
					val = await web.LoadFromWebAsync("https://aqwwiki.wikidot.com/" + item2.Attributes["href"].Value);
					HtmlNode val2 = val.DocumentNode.SelectSingleNode("//div[@id='page-content']");
					if (val2.InnerText.Contains($"Level: {item.Level}") && val2.InnerText.Contains("Total HP:") && val2.InnerText.Contains(" " + item.MaxHealth.ToString("N", new CultureInfo("en-US")).Split('.')[0]))
					{
						targeted = val.DocumentNode.SelectNodes("//div[@id='page-content']");
						break;
					}
				}
			}
			if (targeted[0].SelectSingleNode("div").SelectNodes("div[@class='yui-content']") != null)
			{
				foreach (HtmlNode item3 in (IEnumerable<HtmlNode>)targeted[0].SelectSingleNode("div").SelectNodes("div[@class='yui-content']")[0].ChildNodes)
				{
					if (item3.InnerText.Contains(Player.Map.Substring(0, 1).ToUpper() + Player.Map.Substring(1, 2)) && item3.InnerText.Contains($"Level: {item.Level}") && item3.InnerText.Contains("Total HP:") && item3.InnerText.Contains(" " + item.MaxHealth.ToString("N", new CultureInfo("en-US")).Split('.')[0]))
					{
						targeted = item3.ChildNodes;
						isTabbed = true;
						break;
					}
				}
			}
			int num = (isTabbed ? targeted.GetNodeIndex(((IEnumerable<HtmlNode>)targeted).FirstOrDefault((HtmlNode n) => n.InnerText.Equals("Temporary Items Dropped:", StringComparison.OrdinalIgnoreCase))) : targeted[0].ChildNodes.GetNodeIndex(((IEnumerable<HtmlNode>)targeted[0].ChildNodes).FirstOrDefault((HtmlNode n) => n.InnerText.Equals("Temporary Items Dropped:", StringComparison.OrdinalIgnoreCase))));
			if (num > -1)
			{
				TreeNode treeNode3 = treeNode2.Nodes.Add("Temporary Items");
				HtmlNodeCollection val3 = (isTabbed ? targeted[num + 2].ChildNodes : targeted[0].ChildNodes[num + 2].ChildNodes);
				foreach (HtmlNode item4 in (IEnumerable<HtmlNode>)val3)
				{
					if (val3.Count == 0 || item4.InnerText.Equals("N/A", StringComparison.OrdinalIgnoreCase) || item4.InnerText.Equals("Items Dropped:", StringComparison.OrdinalIgnoreCase))
					{
						treeNode3.Nodes.Add("N/A");
						break;
					}
					if (!string.IsNullOrWhiteSpace(item4.InnerText) && !item4.OuterHtml.Contains("raresmall.png") && !item4.OuterHtml.Contains("text-decoration: line-through"))
					{
						string text = item4.InnerText.Split(new string[1] { item4.InnerText.Contains(" (Dropped") ? " (Dropped" : (item4.InnerText.Contains(" (dropped") ? " (dropped" : " (During") }, StringSplitOptions.None)[0];
						text = text.Split(new string[1] { " x" }, StringSplitOptions.None)[0];
						int startIndex = text.LastIndexOf(' ');
						TreeNode treeNode4 = treeNode3.Nodes.Add(text.EndsWith(" ") ? text.Remove(startIndex) : text);
						treeNode4.Nodes.Add("Legend-tagged: " + (item4.OuterHtml.Contains("legendsmall.png") ? "True" : "False"));
						treeNode4.Nodes.Add("Seasonal-tagged: " + (item4.OuterHtml.Contains("seasonalsmall.png") ? "True" : "False"));
						if (item4.InnerText.Contains(" (Dropped") || item4.InnerText.Contains(" (dropped") || item4.InnerText.Contains(" (During"))
						{
							treeNode4.Nodes.Add("Note: Dropped during the '" + item4.ChildNodes["a"].LastChild.InnerText + "' quest.");
						}
					}
				}
			}
			int num2 = (isTabbed ? targeted.GetNodeIndex(((IEnumerable<HtmlNode>)targeted).FirstOrDefault((HtmlNode n) => n.InnerText.Equals("Items Dropped:", StringComparison.OrdinalIgnoreCase))) : targeted[0].ChildNodes.GetNodeIndex(((IEnumerable<HtmlNode>)targeted[0].ChildNodes).FirstOrDefault((HtmlNode n) => n.InnerText.Equals("Items Dropped:", StringComparison.OrdinalIgnoreCase))));
			if (num2 > -1)
			{
				TreeNode treeNode5 = treeNode2.Nodes.Add("Items");
				HtmlNodeCollection val4 = (isTabbed ? targeted[num2 + 2].ChildNodes : targeted[0].ChildNodes[num2 + 2].ChildNodes);
				foreach (HtmlNode item5 in (IEnumerable<HtmlNode>)val4)
				{
					if (val4.Count == 0 || item5.InnerText.Equals("N/A", StringComparison.OrdinalIgnoreCase))
					{
						treeNode5.Nodes.Add("N/A");
						break;
					}
					if (!string.IsNullOrWhiteSpace(item5.InnerText) && !item5.OuterHtml.Contains("raresmall.png") && !item5.OuterHtml.Contains("text-decoration: line-through"))
					{
						string ınnerText = item5.ChildNodes["a"].FirstChild.InnerText;
						int startIndex2 = ınnerText.LastIndexOf(' ');
						TreeNode treeNode6 = treeNode5.Nodes.Add(ınnerText.EndsWith(" ") ? ınnerText.Remove(startIndex2) : ınnerText);
						treeNode6.ContextMenuStrip = Wiki(ınnerText.EndsWith(" ") ? ınnerText.Remove(startIndex2) : ınnerText);
						treeNode6.ContextMenuStrip = MenuItem(new InventoryItem
						{
							Name = (ınnerText.EndsWith(" ") ? ınnerText.Remove(startIndex2) : ınnerText)
						});
						treeNode6.Nodes.Add("AC-tagged: " + (item5.OuterHtml.Contains("acsmall.png") ? "True" : "False"));
						treeNode6.Nodes.Add("Legend-tagged: " + (item5.OuterHtml.Contains("legendsmall.png") ? "True" : "False"));
						treeNode6.Nodes.Add("Seasonal-tagged: " + (item5.OuterHtml.Contains("seasonalsmall.png") ? "True" : "False"));
						if (item5.InnerText.Contains(" (Dropped") || item5.InnerText.Contains(" (dropped") || item5.InnerText.Contains(" (During"))
						{
							treeNode6.Nodes.Add("Note: Dropped during the '" + item5.ChildNodes["a"].LastChild.InnerText + "' quest.");
						}
					}
				}
			}
			if (treeNode2.Nodes.Count == 0)
			{
				treeNode2.Nodes.Add("N/A");
			}
		}
		catch (Exception)
		{
			treeNode2.Nodes.Clear();
			treeNode2.Nodes.Add("The Wiki is not available at the moment.");
		}
		treeNode2.Nodes.RemoveAt(0);
		treeNode2.ForeColor = Color.Gainsboro;
	}

	private static ContextMenuStrip Wiki(string item)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
		{
			Text = "Go to Wiki page"
		};
		ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
		{
			Text = "Search on Wiki"
		};
		toolStripMenuItem.Click += delegate
		{
			Search("https://aqwwiki.wikidot.com/" + item.Replace(" ", "+"));
		};
		toolStripMenuItem2.Click += delegate
		{
			Search("https://aqwwiki.wikidot.com/search:site/q/" + item.Replace(" ", "+"));
		};
		contextMenuStrip.Items.Add(toolStripMenuItem);
		contextMenuStrip.Items.Add(toolStripMenuItem2);
		return contextMenuStrip;
	}

	private static ContextMenuStrip Wiki(ShopInfo item)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
		{
			Text = "Go to Wiki page"
		};
		ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
		{
			Text = "Search on Wiki"
		};
		ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
		{
			Text = "Load Shop"
		};
		toolStripMenuItem.Click += delegate
		{
			Search("https://aqwwiki.wikidot.com/" + item.Name.Replace(" ", "+"));
		};
		toolStripMenuItem2.Click += delegate
		{
			Search("https://aqwwiki.wikidot.com/search:site/q/" + item.Name.Replace(" ", "+"));
		};
		toolStripMenuItem3.Click += delegate
		{
			Shop.Load(item.Id);
		};
		contextMenuStrip.Items.Add(toolStripMenuItem);
		contextMenuStrip.Items.Add(toolStripMenuItem2);
		contextMenuStrip.Items.Add(toolStripMenuItem3);
		return contextMenuStrip;
	}

	private static ContextMenuStrip Wiki(InventoryItem Item)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
		{
			Text = "Go to Wiki page"
		};
		ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
		{
			Text = "Search on Wiki"
		};
		ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
		{
			Text = "Equip SWF"
		};
		toolStripMenuItem.Click += delegate
		{
			Search("https://aqwwiki.wikidot.com/" + Item.Name.Replace(" ", "+"));
		};
		toolStripMenuItem2.Click += delegate
		{
			Search("https://aqwwiki.wikidot.com/search:site/q/" + Item.Name.Replace(" ", "+"));
		};
		toolStripMenuItem3.Click += delegate
		{
			string category = Item.Category;
			string text;
			switch (category)
			{
			case "Cape":
				text = "ba";
				break;
			case "Pet":
				text = "pe";
				break;
			case "Armor":
			case "Class":
				text = "co";
				break;
			case "Helm":
				text = "he";
				break;
			case "Misc":
				text = "mi";
				break;
			default:
				text = "Weapon";
				break;
			}
			dynamic val = new ExpandoObject();
			val.sFile = Item.File;
			val.sLink = Item.Link;
			val.sType = category;
			Flash.Call("SetEquip", new object[2] { text, val });
		};
		contextMenuStrip.Items.Add(toolStripMenuItem);
		contextMenuStrip.Items.Add(toolStripMenuItem2);
		if (Item.IsWeapon || Item.IsEquippableNonItem)
		{
			contextMenuStrip.Items.Add(toolStripMenuItem3);
		}
		return contextMenuStrip;
	}

	private static ContextMenuStrip MenuQuest(int QuestID)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
		{
			Text = "Add to Quest List"
		};
		ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
		{
			Text = "Accept Quest"
		};
		ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
		{
			Text = "Complete Quest"
		};
		ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem
		{
			Text = "Load Quest"
		};
		toolStripMenuItem.Click += delegate(object S, EventArgs E)
		{
			AddQuest(S, E, QuestID);
		};
		toolStripMenuItem2.Click += delegate
		{
			Player.Quests.Accept(QuestID);
		};
		toolStripMenuItem3.Click += delegate
		{
			Player.Quests.Complete(QuestID);
		};
		toolStripMenuItem4.Click += delegate
		{
			Player.Quests.Load(QuestID);
		};
		contextMenuStrip.Items.Add(toolStripMenuItem);
		contextMenuStrip.Items.Add(toolStripMenuItem2);
		contextMenuStrip.Items.Add(toolStripMenuItem3);
		contextMenuStrip.Items.Add(toolStripMenuItem4);
		return contextMenuStrip;
	}

	private static ContextMenuStrip MenuItems(List<InventoryItem> Items)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
		{
			Text = "Add all to Drops and Items List"
		};
		ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
		{
			Text = "Add all to Drops List"
		};
		ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
		{
			Text = "Add all to Items list"
		};
		ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem
		{
			Text = "Search all on Wiki"
		};
		toolStripMenuItem.Click += delegate(object S, EventArgs E)
		{
			AddDrops(S, E, Items);
			AddItems(S, E, Items);
		};
		toolStripMenuItem2.Click += delegate(object S, EventArgs E)
		{
			AddDrops(S, E, Items);
		};
		toolStripMenuItem3.Click += delegate(object S, EventArgs E)
		{
			AddDrops(S, E, Items);
		};
		toolStripMenuItem4.Click += delegate
		{
			foreach (InventoryItem Item in Items)
			{
				Process.Start("https://aqwwiki.wikidot.com/search:site/q/" + Item.Name.Replace(" ", "+"));
			}
		};
		contextMenuStrip.Items.Add(toolStripMenuItem);
		contextMenuStrip.Items.Add(toolStripMenuItem2);
		contextMenuStrip.Items.Add(toolStripMenuItem3);
		contextMenuStrip.Items.Add(toolStripMenuItem4);
		return contextMenuStrip;
	}

	private static ContextMenuStrip MenuItem(InventoryItem Item)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
		{
			Text = "Add to Drops and Items List"
		};
		ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
		{
			Text = "Add to Drops List"
		};
		ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
		{
			Text = "Add to Items List"
		};
		ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem
		{
			Text = "Copy item name to Clipboard"
		};
		ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem
		{
			Text = "Go to Wiki page"
		};
		ToolStripMenuItem toolStripMenuItem6 = new ToolStripMenuItem
		{
			Text = "Search on Wiki"
		};
		toolStripMenuItem.Click += delegate(object S, EventArgs E)
		{
			AddDrop(S, E, Item);
			AddItem(S, E, Item);
		};
		toolStripMenuItem2.Click += delegate(object S, EventArgs E)
		{
			AddDrop(S, E, Item);
		};
		toolStripMenuItem3.Click += delegate(object S, EventArgs E)
		{
			AddItem(S, E, Item);
		};
		toolStripMenuItem4.Click += delegate
		{
			Clipboard.SetText(Item.Name);
		};
		toolStripMenuItem5.Click += delegate
		{
			Search("https://aqwwiki.wikidot.com/" + Item.Name.Replace(" ", "+"));
		};
		toolStripMenuItem6.Click += delegate
		{
			Search("https://aqwwiki.wikidot.com/search:site/q/" + Item.Name.Replace(" ", "+"));
		};
		contextMenuStrip.Items.Add(toolStripMenuItem);
		contextMenuStrip.Items.Add(toolStripMenuItem2);
		contextMenuStrip.Items.Add(toolStripMenuItem3);
		contextMenuStrip.Items.Add(toolStripMenuItem4);
		contextMenuStrip.Items.Add(toolStripMenuItem5);
		contextMenuStrip.Items.Add(toolStripMenuItem6);
		return contextMenuStrip;
	}

	private static ContextMenuStrip MenuItem(ItemBase Item)
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
		{
			Text = "Add to Drops and Items List"
		};
		ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
		{
			Text = "Add to Drops List"
		};
		ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
		{
			Text = "Add to Items List"
		};
		ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem
		{
			Text = "Copy item name to Clipboard"
		};
		ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem
		{
			Text = "Go to Wiki page"
		};
		ToolStripMenuItem toolStripMenuItem6 = new ToolStripMenuItem
		{
			Text = "Search on Wiki"
		};
		ToolStripMenuItem toolStripMenuItem7 = new ToolStripMenuItem
		{
			Text = "Equip SWF"
		};
		toolStripMenuItem.Click += delegate(object S, EventArgs E)
		{
			AddDrop(S, E, Item);
			AddItem(S, E, Item);
		};
		toolStripMenuItem2.Click += delegate(object S, EventArgs E)
		{
			AddDrop(S, E, Item);
		};
		toolStripMenuItem3.Click += delegate(object S, EventArgs E)
		{
			AddItem(S, E, Item);
		};
		toolStripMenuItem4.Click += delegate
		{
			Clipboard.SetText(Item.Name);
		};
		toolStripMenuItem5.Click += delegate
		{
			Search("https://aqwwiki.wikidot.com/" + Item.Name.Replace(" ", "+"));
		};
		toolStripMenuItem6.Click += delegate
		{
			Search("https://aqwwiki.wikidot.com/search:site/q/" + Item.Name.Replace(" ", "+"));
		};
		toolStripMenuItem7.Click += delegate
		{
			string category = Item.Category;
			string text;
			switch (category)
			{
			case "Cape":
				text = "ba";
				break;
			case "Pet":
				text = "pe";
				break;
			case "Armor":
			case "Class":
				text = "co";
				break;
			case "Helm":
				text = "he";
				break;
			case "Misc":
				text = "mi";
				break;
			default:
				text = "Weapon";
				break;
			}
			dynamic val = new ExpandoObject();
			val.sFile = Item.File;
			val.sLink = Item.Link;
			val.sType = category;
			Flash.Call("SetEquip", new object[2] { text, val });
		};
		contextMenuStrip.Items.Add(toolStripMenuItem);
		contextMenuStrip.Items.Add(toolStripMenuItem2);
		contextMenuStrip.Items.Add(toolStripMenuItem3);
		contextMenuStrip.Items.Add(toolStripMenuItem4);
		contextMenuStrip.Items.Add(toolStripMenuItem5);
		contextMenuStrip.Items.Add(toolStripMenuItem6);
		contextMenuStrip.Items.Add(toolStripMenuItem7);
		return contextMenuStrip;
	}

	private static void Search(string Item)
	{
		Process.Start(Item);
	}

	private static void AddDrop(object s, EventArgs e, InventoryItem Item)
	{
		if (!Item.IsTemporary)
		{
			BotManager.Instance.AddDrop(Item.Name);
		}
	}

	private static void AddItem(object s, EventArgs e, InventoryItem Item)
	{
		if (!Item.IsTemporary)
		{
			BotManager.Instance.AddItem(Item.Name);
		}
	}

	private static void AddDrops(object s, EventArgs e, List<InventoryItem> Items)
	{
		foreach (InventoryItem Item in Items)
		{
			AddDrop(s, e, Item);
		}
	}

	private static void AddItems(object s, EventArgs e, List<InventoryItem> Items)
	{
		foreach (InventoryItem Item in Items)
		{
			AddItem(s, e, Item);
		}
	}

	private static void AddDrop(object s, EventArgs e, ItemBase Item)
	{
		if (!Item.Temp)
		{
			BotManager.Instance.AddDrop(Item.Name);
		}
	}

	private static void AddItem(object s, EventArgs e, ItemBase Item)
	{
		if (!Item.Temp)
		{
			BotManager.Instance.AddItem(Item.Name);
		}
	}

	private static void AddDrops(object s, EventArgs e, List<ItemBase> Items)
	{
		foreach (ItemBase Item in Items)
		{
			AddDrop(s, e, Item);
		}
	}

	private static void AddItems(object s, EventArgs e, List<ItemBase> Items)
	{
		foreach (ItemBase Item in Items)
		{
			AddItem(s, e, Item);
		}
	}

	private static void AddQuest(object s, EventArgs e, int ID)
	{
		BotManager.Instance.AddQuest(ID);
	}
}
