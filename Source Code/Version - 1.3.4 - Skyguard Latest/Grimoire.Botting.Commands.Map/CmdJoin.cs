using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Networking;

namespace Grimoire.Botting.Commands.Map;

public class CmdJoin : IBotCommand
{
	public string Map { get; set; }

	public string Cell { get; set; }

	public string Pad { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Move;
		await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
		string cmdMap = (Map.Contains("-") ? Map.Split('-')[0] : Map);
		string text = Map.Substring(cmdMap.Length);
		bool checkVar = instance.IsVar(text.Replace("-", ""));
		if (text.Contains("Packet"))
		{
			await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
			if (!instance.IsRunning || !Player.IsAlive || !Player.IsLoggedIn)
			{
				return;
			}
			string username = Player.Username;
			await Proxy.Instance.SendToServer("%xt%zm%cmd%1%tfer%" + username + "%" + cmdMap + "-100000");
			await instance.WaitUntil(() => !World.IsMapLoading, null, 40);
			await Task.Delay(1000);
		}
		if (!cmdMap.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
		{
			if (!checkVar)
			{
				await TryJoin(instance, cmdMap, text);
			}
			else
			{
				text = "-" + Configuration.Tempvariable[instance.GetVar(text.Replace("-", ""))];
				await TryJoin(instance, cmdMap, text);
			}
		}
		if (cmdMap.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
		{
			if (!Player.Cell.Equals(Cell, StringComparison.OrdinalIgnoreCase) && !text.Contains("Packet"))
			{
				Player.MoveToCell(Cell, Pad);
				await Task.Delay(500);
			}
			BotData.BotMap = cmdMap;
			BotData.BotCell = Cell;
			BotData.BotPad = Pad;
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public async Task TryJoin(IBotEngine instance, string MapName, string RoomProp = "")
	{
		await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
		if (Player.CurrentState == Player.State.InCombat)
		{
			Player.MoveToCell(Player.Cell, Player.Pad);
			await Task.Delay(1250);
		}
		RoomProp = new Regex("-{1,}", RegexOptions.IgnoreCase).Replace(RoomProp, (Match m) => "-");
		RoomProp = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(RoomProp, (Match m) => new Random().Next(1001, 99999).ToString());
		Player.JoinMap(MapName + RoomProp, Cell, Pad);
		await instance.WaitUntil(() => Player.Map.Equals(MapName, StringComparison.OrdinalIgnoreCase), null, 5);
		await instance.WaitUntil(() => !World.IsMapLoading, null, 40);
	}

	public override string ToString()
	{
		return "Join: " + Map + ", " + Cell + ", " + Pad;
	}
}
