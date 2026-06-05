using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdHealthLessThan : StatementCommand, IBotCommand
{
	public CmdHealthLessThan()
	{
		base.Tag = "This player";
		base.Text = "Health is less than";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.Health >= int.Parse(instance.Value(base.Value1)))
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
		return "Health is less than: " + base.Value1;
	}
}
