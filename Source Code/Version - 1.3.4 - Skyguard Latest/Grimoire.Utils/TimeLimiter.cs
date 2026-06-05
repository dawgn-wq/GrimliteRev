using System;
using System.Collections.Generic;

namespace Grimoire.Utils;

public class TimeLimiter
{
	private Dictionary<string, int> _last = new Dictionary<string, int>();

	public bool LimitedRun(string name, int delay, Action action)
	{
		int value;
		bool flag = !_last.TryGetValue(name, out value) || Environment.TickCount - value >= delay;
		if (flag)
		{
			action();
			_last[name] = Environment.TickCount;
		}
		return flag;
	}
}
