using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdLevelIs : StatementCommand, IBotCommand
{
	public CmdLevelIs()
	{
		base.Tag = "This player";
		base.Text = "Level is";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.Level != int.Parse(instance.Value(base.Value1)))
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
		return "Level is equal to: " + base.Value1;
	}
}
