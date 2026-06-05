using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdHealthGreaterThan : StatementCommand, IBotCommand
{
	public CmdHealthGreaterThan()
	{
		base.Tag = "This player";
		base.Text = "Health is greater than";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.Health <= int.Parse(instance.Value(base.Value1)))
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
		return "Health is greater than: " + base.Value1;
	}
}
