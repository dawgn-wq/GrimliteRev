using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdLevelGreaterThan : StatementCommand, IBotCommand
{
	public CmdLevelGreaterThan()
	{
		base.Tag = "This player";
		base.Text = "Level is greater than";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.Level <= int.Parse(instance.Value(base.Value1)))
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
		return "Level is greater than: " + base.Value1;
	}
}
