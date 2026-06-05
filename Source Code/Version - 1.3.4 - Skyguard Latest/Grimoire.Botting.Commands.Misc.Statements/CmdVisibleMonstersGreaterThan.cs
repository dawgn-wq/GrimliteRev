using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdVisibleMonstersGreaterThan : StatementCommand, IBotCommand
{
	public CmdVisibleMonstersGreaterThan()
	{
		base.Tag = "Monster";
		base.Text = "Visible count is greater than";
	}

	public Task Execute(IBotEngine instance)
	{
		try
		{
			if (World.VisibleMonster(Player.Cell).Count <= int.Parse(instance.Value(base.Value1)))
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
		return "Visible monster count is greater than: " + base.Value1;
	}
}
