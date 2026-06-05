using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdVisibleMonstersLessThan : StatementCommand, IBotCommand
{
	public CmdVisibleMonstersLessThan()
	{
		base.Tag = "Monster";
		base.Text = "Visible count is less than";
	}

	public Task Execute(IBotEngine instance)
	{
		try
		{
			if (World.VisibleMonster(Player.Cell).Count >= int.Parse(instance.Value(base.Value1)))
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
		return "Visible monster count is less than: " + base.Value1;
	}
}
