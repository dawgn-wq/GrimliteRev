using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Tools;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Misc;

public class CmdGotoPlayer : RegularExpression, IBotCommand
{
	private string _map;

	public string PlayerName { get; set; }

	private string _playerName => BotManager.Instance.ActiveBotEngine.Value(PlayerName);

	public async Task Execute(IBotEngine instance)
	{
		if (World.PlayersInMap.FirstOrDefault((string name) => name.Equals(_playerName, StringComparison.OrdinalIgnoreCase)) == null)
		{
			BotData.BotState = BotData.State.Move;
			await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer), null, 10, 500);
			_map = Player.Map;
			Player.GoToPlayer(_playerName);
			await instance.WaitUntil(() => Player.Map.Equals(_map, StringComparison.OrdinalIgnoreCase), null, 10, 500);
			await instance.WaitUntil(() => !World.IsMapLoading, null, 10, 500);
			if (AutoRelogin.IsClientLoading("MapLoadingStuck") || AutoRelogin.IsClientLoading("MapLoadingError"))
			{
				World.ReloadCurrentMap();
			}
			World.SetSpawnPoint();
			BotData.BotMap = Player.Map;
			BotData.BotCell = Player.Cell;
			BotData.BotPad = Player.Pad;
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Goto player: " + PlayerName;
	}
}
