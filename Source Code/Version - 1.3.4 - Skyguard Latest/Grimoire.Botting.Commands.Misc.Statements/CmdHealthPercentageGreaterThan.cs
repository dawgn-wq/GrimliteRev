using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdHealthPercentageGreaterThan : StatementCommand, IBotCommand
{
	public CmdHealthPercentageGreaterThan()
	{
		base.Tag = "This player";
		base.Text = "Health percentage is greater than";
	}

	public Task Execute(IBotEngine instance)
	{
		if ((double)Player.Health / (double)Player.HealthMax * 100.0 <= (double)int.Parse(instance.Value(base.Value1)))
		{
			instance.Index++;
		}
		return Task.FromResult<object>(null);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Health percentage is greater than: " + base.Value1;
	}
}
