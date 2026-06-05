using Newtonsoft.Json;

namespace Grimoire.Game.Data;

public class Faction
{
	[JsonProperty("FactionID")]
	public int ID { get; set; }

	[JsonProperty("sName")]
	public string Name { get; set; }

	public int Rank => CalculatePoints(TotalRep);

	[JsonProperty("iRep")]
	public int TotalRep { get; set; }

	public int Rep
	{
		get
		{
			if (Rank == 10)
			{
				return 0;
			}
			return TotalRep - CalculateRank(Rank, add: true);
		}
	}

	public int RequiredRep => CalculateRank(Rank, add: false);

	public int RemainingRep => RequiredRep - Rep;

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

	public int CalculateRank(int value, bool add)
	{
		switch (value)
		{
		case 10:
			return 0;
		case 9:
			if (!add)
			{
				return 100000;
			}
			return 302500;
		case 8:
			if (!add)
			{
				return 72900;
			}
			return 202500;
		case 7:
			if (!add)
			{
				return 51200;
			}
			return 129600;
		case 6:
			if (!add)
			{
				return 34300;
			}
			return 78400;
		case 5:
			if (!add)
			{
				return 21600;
			}
			return 44100;
		case 4:
			if (!add)
			{
				return 12500;
			}
			return 22500;
		case 3:
			if (!add)
			{
				return 6400;
			}
			return 10000;
		case 2:
			if (!add)
			{
				return 2700;
			}
			return 3600;
		case 1:
			return 900;
		default:
			return 0;
		}
	}
}
