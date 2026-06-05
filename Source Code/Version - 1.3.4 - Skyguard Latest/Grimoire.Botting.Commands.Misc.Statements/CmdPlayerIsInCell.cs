using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayerIsInCell : StatementCommand, IBotCommand
{
	private string _cell => BotManager.Instance.ActiveBotEngine.Value(base.Value2);

	public CmdPlayerIsInCell()
	{
		base.Tag = "Player";
		base.Text = "Player is in cell";
	}

	public Task Execute(IBotEngine instance)
	{
		try
		{
			if (World.AvatarsInCell(_cell).FirstOrDefault((Avatar p) => p.Name.Equals(instance.Value(base.Value1), StringComparison.OrdinalIgnoreCase)) == null)
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
		return "Player is in " + base.Value2 + ": " + base.Value1;
	}
}
