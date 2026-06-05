using System;
using System.Windows.Forms;

namespace Grimoire.Utils;

public static class ControlUtils
{
	public static bool CheckedInvoke(this Control c, Action a)
	{
		bool ınvokeRequired = c.InvokeRequired;
		(ınvokeRequired ? ((Action)delegate
		{
			c.Invoke(a);
		}) : a)();
		return ınvokeRequired;
	}
}
