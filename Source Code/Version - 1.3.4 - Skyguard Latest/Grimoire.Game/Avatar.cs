using System.Collections.Generic;
using Grimoire.Botting;
using Grimoire.Game.Data;
using Newtonsoft.Json;

namespace Grimoire.Game;

public class Avatar
{
	public enum State
	{
		Dead,
		Idle,
		InCombat
	}

	public enum Access
	{
		Player = 0,
		Staff = 30,
		Tester = 40,
		Developer = 50,
		Moderator = 60,
		LeadDeveloper = 100
	}

	[JsonProperty("uoName")]
	public string Name { get; set; }

	[JsonProperty("strUsername")]
	public string Username { get; set; }

	[JsonProperty("strGender")]
	public string Gender { get; set; }

	[JsonProperty("strClassName")]
	public string Class { get; set; }

	public int ClassRank => CalculatePoints(CP);

	[JsonProperty("iCP")]
	public int CP { get; set; }

	[JsonProperty("eqp")]
	public Dictionary<string, CosmeticEquipment> EquippedItems { get; set; }

	[JsonProperty("intHP")]
	public int HP { get; set; }

	[JsonProperty("intHPMax")]
	public int MaxHP { get; set; }

	[JsonProperty("intMP")]
	public int MP { get; set; }

	[JsonProperty("intMPMax")]
	public int MaxMP { get; set; }

	[JsonProperty("intSP")]
	public int SP { get; set; }

	[JsonProperty("intSPMax")]
	public int MaxSP { get; set; }

	[JsonProperty("afk")]
	public bool AFK { get; set; }

	public bool Member
	{
		get
		{
			if (UpgDays >= 0)
			{
				return true;
			}
			return false;
		}
	}

	[JsonProperty("iUpgDays")]
	public int UpgDays { get; set; }

	[JsonProperty("uid")]
	public int UserID { get; set; }

	[JsonProperty("entID")]
	public int EntID { get; set; }

	[JsonProperty("entType")]
	public string EntType { get; set; }

	public Access AccessType => (Access)AccessLevel;

	[JsonProperty("intAccessLevel")]
	public int AccessLevel { get; set; }

	[JsonProperty("intLevel")]
	public int Level { get; set; }

	[JsonProperty("strFrame")]
	public string Cell { get; set; }

	[JsonProperty("strPad")]
	public string Pad { get; set; }

	[JsonProperty("tx")]
	public float X { get; set; }

	[JsonProperty("ty")]
	public float Y { get; set; }

	public float[] Position => new float[2] { X, Y };

	[JsonProperty("intState")]
	public int CurrentState { get; set; }

	public ExtendedAvatar Extended { get; set; }

	public List<Aura> Auras { get; set; }

	public List<Monster> Targets { get; set; }

	public int CalculatePoints(int value)
	{
		if (value >= 302500)
		{
			return 10;
		}
		if (value >= 202500)
		{
			return 9;
		}
		if (value >= 129600)
		{
			return 8;
		}
		if (value >= 78400)
		{
			return 7;
		}
		if (value >= 44100)
		{
			return 6;
		}
		if (value >= 22500)
		{
			return 5;
		}
		if (value >= 10000)
		{
			return 4;
		}
		if (value >= 3600)
		{
			return 3;
		}
		if (value >= 900)
		{
			return 2;
		}
		if (value < 900)
		{
			return 1;
		}
		return 0;
	}

	public override string ToString()
	{
		return $"{EntID}: {Name}";
	}
}
