using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Grimoire.Game.Data;

public class Map
{
	[JsonProperty("areaId")]
	public static int ID { get; set; }

	[JsonProperty("strMapName")]
	public string Name { get; set; }

	[JsonProperty("areaName")]
	public static string Area { get; set; }

	public static string Instance => Area.Split('-')[1];

	[JsonProperty("strMapFileName")]
	public string FilePath { get; set; }

	public string File => "https://game.aq.com/game/gamefiles/maps/" + FilePath;

	[JsonProperty("intType")]
	public int Type { get; set; }

	public List<Monster> Monsters { get; set; }

	public List<Cell> Cells { get; set; } = new List<Cell>();

	public List<string> EmptyCells
	{
		get
		{
			List<string> list = new List<string>();
			string[] cells = World.Cells;
			foreach (string cell in cells)
			{
				if (cell != "Wait" && cell != "Blank" && World.Monsters.FirstOrDefault((Monster m) => m.Cell.Equals(cell, StringComparison.OrdinalIgnoreCase)) == null)
				{
					list.Add(cell);
				}
			}
			if (list.Count == 0)
			{
				list.Add("Enter");
			}
			return list;
		}
	}

	public List<MapItem> MapItems
	{
		get
		{
			List<MapItem> list = new List<MapItem>();
			foreach (Cell cell in Cells)
			{
				list.AddRange(cell.MapItems);
			}
			return list;
		}
	}
}
