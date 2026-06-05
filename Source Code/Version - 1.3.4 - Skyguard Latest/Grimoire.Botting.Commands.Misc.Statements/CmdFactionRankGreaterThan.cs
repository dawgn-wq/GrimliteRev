using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdFactionRankGreaterThan : StatementCommand, IBotCommand
{
	public CmdFactionRankGreaterThan()
	{
		base.Tag = "This player";
		base.Text = "Faction Rank is greater than";
	}

	public Task Execute(IBotEngine instance)
	{
		if ((Player.Factions.Find((Faction m) => m.Name == instance.Value(base.Value1)) ?? new Faction()).Rank <= int.Parse(instance.Value(base.Value2)))
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
		return "Faction Rank is greater than: " + base.Value1 + ", " + base.Value2;
	}
}
