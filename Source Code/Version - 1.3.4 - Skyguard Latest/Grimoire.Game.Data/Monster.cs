using System.Collections.Generic;
using Newtonsoft.Json;

namespace Grimoire.Game.Data;

public class Monster
{
	public enum State
	{
		Dead,
		Idle,
		InCombat
	}

	private string _name;

	[JsonProperty("sRace")]
	public string Race { get; set; }

	[JsonProperty("strMonName")]
	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value?.Trim();
		}
	}

	[JsonProperty("MonID")]
	public int Id { get; set; }

	[JsonProperty("iLvl")]
	public int Level { get; set; }

	[JsonProperty("intState")]
	public int CurrentState { get; set; }

	[JsonProperty("intHP")]
	public int Health { get; set; }

	[JsonProperty("intHPMax")]
	public int MaxHealth { get; set; }

	[JsonProperty("intMP")]
	public int Mana { get; set; }

	[JsonProperty("intMPMax")]
	public int MaxMana { get; set; }

	[JsonProperty("strFrame")]
	public string Cell { get; set; }

	[JsonProperty("MonMapID")]
	public int MonMapID { get; set; }

	public bool Alive => Health > 0;

	public List<Aura> Auras { get; set; }
}
