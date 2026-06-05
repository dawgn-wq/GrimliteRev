using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdGoldLessThan : StatementCommand, IBotCommand
{
	public CmdGoldLessThan()
	{
		base.Tag = "This player";
		base.Text = "Gold is less than";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.Gold >= int.Parse(instance.Value(base.Value1)))
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
		return "Gold is less than: " + base.Value1;
	}
}
