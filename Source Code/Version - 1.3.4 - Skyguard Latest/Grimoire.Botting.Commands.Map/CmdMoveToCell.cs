using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Map;

public class CmdMoveToCell : IBotCommand
{
	public string Cell { get; set; }

	public string Pad { get; set; }

	private string _cell => BotManager.Instance.ActiveBotEngine.Value(Cell);

	private string _pad => BotManager.Instance.ActiveBotEngine.Value(Pad);

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Move;
		Player.MoveToCell(_cell, _pad);
		BotData.BotCell = _cell;
		BotData.BotPad = _pad;
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Move to Cell: " + Cell + ", " + Pad;
	}
}
