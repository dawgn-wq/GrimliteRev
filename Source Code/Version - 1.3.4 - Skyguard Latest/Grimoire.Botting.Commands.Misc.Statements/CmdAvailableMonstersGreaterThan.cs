using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdAvailableMonstersGreaterThan : StatementCommand, IBotCommand
{
	public CmdAvailableMonstersGreaterThan()
	{
		base.Tag = "Monster";
		base.Text = "Available count is greater than";
	}

	public Task Execute(IBotEngine instance)
	{
		try
		{
			if (World.MonstersInCell(Player.Cell).Count <= int.Parse(instance.Value(base.Value1)))
			{
				instance.Index++;
			}
		}
		catch
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
		return "Available monster count is greater than: " + base.Value1;
	}
}
