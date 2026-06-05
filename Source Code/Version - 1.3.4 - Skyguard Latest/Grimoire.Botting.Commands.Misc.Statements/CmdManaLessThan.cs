using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdManaLessThan : StatementCommand, IBotCommand
{
	public CmdManaLessThan()
	{
		base.Tag = "This player";
		base.Text = "Mana is less than";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.Mana >= int.Parse(instance.Value(base.Value1)))
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
		return "Mana is less than: " + base.Value1;
	}
}
