using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayersInCellGreaterThan : StatementCommand, IBotCommand
{
	private string _cell => BotManager.Instance.ActiveBotEngine.Value(base.Value1);

	public CmdPlayersInCellGreaterThan()
	{
		base.Tag = "Player";
		base.Text = "Player count in cell is greater than";
	}

	public Task Execute(IBotEngine instance)
	{
		try
		{
			if (World.AvatarsInCell(_cell).Count <= int.Parse(instance.Value(base.Value2)))
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
		return "Player count in " + base.Value1 + " is greater than: " + base.Value2;
	}
}
