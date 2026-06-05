using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayersLessThan : StatementCommand, IBotCommand
{
	public CmdPlayersLessThan()
	{
		base.Tag = "Player";
		base.Text = "Count is less than";
	}

	public Task Execute(IBotEngine instance)
	{
		if (World.PlayersInMap.Count >= int.Parse(instance.Value(base.Value1)))
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
		return "Player count is less than: " + base.Value1;
	}
}
