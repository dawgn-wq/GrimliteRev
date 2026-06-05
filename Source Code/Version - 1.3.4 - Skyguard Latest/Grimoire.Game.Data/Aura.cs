using System;
using System.Threading.Tasks;

namespace Grimoire.Game.Data;

public class Aura
{
	public DateTime timeNow;

	public int durationSpan;

	public int entityID { get; set; }

	public string Name { get; set; }

	public int Value { get; set; }

	public string realValue { get; set; }

	public string disableType { get; set; }

	public int Duration
	{
		get
		{
			return durationSpan - time().Seconds;
		}
		set
		{
		}
	}

	public TimeSpan time()
	{
		return DateTime.Now - timeNow;
	}

	public async void countdown(Aura aura)
	{
		await Task.Delay(durationSpan);
		World.PlayerAuras.Remove(aura);
		World.EnemyAuras.Remove(aura);
		World.auraCountdown -= aura.countdown;
	}
}
