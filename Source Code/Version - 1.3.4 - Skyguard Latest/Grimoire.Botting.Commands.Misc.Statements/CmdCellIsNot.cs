using System;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdCellIsNot : StatementCommand, IBotCommand
{
	private string _cell
	{
		get
		{
			if (!(BotManager.Instance.ActiveBotEngine.Value(base.Value1) != "Blank") || !(BotManager.Instance.ActiveBotEngine.Value(base.Value1) != "Wait"))
			{
				return "Enter";
			}
			return BotManager.Instance.ActiveBotEngine.Value(base.Value1);
		}
	}

	public CmdCellIsNot()
	{
		base.Tag = "Map";
		base.Text = "Cell is not";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Player.Cell.Equals(_cell, StringComparison.OrdinalIgnoreCase))
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
		return "Cell is not: " + base.Value1;
	}
}
