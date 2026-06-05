using Newtonsoft.Json;

namespace Grimoire.Game;

public class PlayerInfo
{
	public enum State
	{
		Dead,
		Idle,
		InCombat
	}

	[JsonProperty("uoName")]
	public string Name { get; set; }

	[JsonProperty("strUsername")]
	public string Username { get; set; }

	[JsonProperty("strClassName")]
	public string Class { get; set; }

	[JsonProperty("intHP")]
	public int HP { get; set; }

	[JsonProperty("intHPMax")]
	public int MaxHP { get; set; }

	[JsonProperty("intMP")]
	public int MP { get; set; }

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
	private int UpgDays { get; set; }

	[JsonProperty("entID")]
	public int EntID { get; set; }

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

	[JsonProperty("intState")]
	public State CurrentState { get; set; }

	public override string ToString()
	{
		return $"{EntID}: {Name}";
	}
}
