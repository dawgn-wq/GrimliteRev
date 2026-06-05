using System;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;
using Grimoire.UI;

namespace Grimoire.Botting;

public class OptionsManager
{
	private static bool _isRunning;

	private static bool _disableAnimation;

	private static bool _lagKiller;

	private static bool _hidePlayers;

	private static bool _infRange;

	private static bool _hideYulgar;

	private static bool _hideRoom;

	private static bool _afk;

	private static bool _afk2;

	private static bool saveState;

	private static readonly string[] empty;

	public static bool IsRunning
	{
		get
		{
			return _isRunning;
		}
		private set
		{
			_isRunning = value;
			OptionsManager.StateChanged?.Invoke(value);
		}
	}

	public static bool ProvokeMonsters { get; set; }

	public static bool ProvokeAllMonster { get; set; }

	public static bool PacketSpam { get; set; }

	public static string ProvokePacket { get; set; }

	public static string SpamPacket { get; set; }

	public static bool EnemyMagnet { get; set; }

	public static bool LagKiller
	{
		get
		{
			return _lagKiller;
		}
		set
		{
			_lagKiller = value;
			SetLagKiller();
		}
	}

	public static bool SkipCutscenes { get; set; }

	public static bool DisableAnimations
	{
		get
		{
			return _disableAnimation;
		}
		set
		{
			_disableAnimation = value;
			RunDisableAnimations(value);
		}
	}

	public static bool HidePlayers
	{
		get
		{
			return _hidePlayers;
		}
		set
		{
			_hidePlayers = value;
			DestroyPlayers(value);
		}
	}

	public static bool InfiniteRange
	{
		get
		{
			return _infRange;
		}
		set
		{
			_infRange = value;
			SetInfiniteRange(value);
		}
	}

	public static int WalkSpeed { get; set; }

	public static int Timer { get; set; }

	public static int PacketDelay { get; set; }

	public static int RoomNumber { get; set; }

	public static bool Untarget { get; set; }

	public static bool AFK
	{
		get
		{
			return _afk;
		}
		set
		{
			_afk = value;
			if (value)
			{
				Proxy.Instance.RegisterHandler(HandlerAFK1);
			}
			else
			{
				Proxy.Instance.UnregisterHandler(HandlerAFK1);
			}
		}
	}

	public static bool HideRoom
	{
		get
		{
			return _hideRoom;
		}
		set
		{
			_hideRoom = value;
			if (value)
			{
				Proxy.Instance.RegisterHandler(HandlerHideRoom);
			}
			else
			{
				Proxy.Instance.UnregisterHandler(HandlerHideRoom);
			}
		}
	}

	public static bool ChangeChat { get; set; }

	public static bool WarningMsgFilter { get; set; }

	public static bool _saveState
	{
		get
		{
			return saveState;
		}
		set
		{
			saveState = value;
			if (value)
			{
				SaveState();
			}
		}
	}

	public static bool _antiAfk { get; set; }

	public static bool HideYulgar
	{
		get
		{
			return _hideYulgar;
		}
		set
		{
			_hideYulgar = value;
			if (value && Player.IsLoggedIn && Player.Map.ToLower() == "yulgar")
			{
				DestroyPlayers(Enabled: true);
			}
		}
	}

	private static IJsonMessageHandler HandlerHideRoom { get; }

	private static IXtMessageHandler HandlerAFK1 { get; }

	public static event Action<bool> StateChanged;

	public static void SetInfiniteRange(bool Toggle)
	{
		Flash.Call("SetInfiniteRange", Toggle);
	}

	public static void SetProvokeMonsters()
	{
		Flash.Call("SetProvokeMonsters", empty);
	}

	public static void SetEnemyMagnet()
	{
		Flash.Call("SetEnemyMagnet", empty);
	}

	public static void SetLagKiller()
	{
		Flash.Call("SetLagKiller", !LagKiller);
	}

	public static void DestroyPlayers(bool Enabled)
	{
		Flash.Call("DestroyPlayers", Enabled);
	}

	public static void SetSkipCutscenes()
	{
		Flash.Call("SetSkipCutscenes", (BotData.BotCell == null) ? "Enter" : BotData.BotCell, (BotData.BotPad == null) ? "Spawn" : BotData.BotPad);
	}

	public static void SetWalkSpeed()
	{
		Flash.Call("SetWalkSpeed", WalkSpeed);
	}

	public static void DisableDeathAds()
	{
		Flash.Call("DisableDeathAd", empty);
	}

	public static void RunDisableAnimations(bool Enabled)
	{
		Flash.Call("DisableAnimations", Enabled);
	}

	public static void Start()
	{
		ApplySettings();
		if (BotManager.Instance.chkLag.Checked)
		{
			LagKiller = true;
			SetLagKiller();
		}
		if (BotManager.Instance.chkProvoke.Checked)
		{
			ProvokeMonsters = true;
		}
		if (BotManager.Instance.chkProvokeAllMon.Checked)
		{
			ProvokeAllMonster = true;
		}
		if (BotManager.Instance.chkSaveState.Checked)
		{
			_saveState = true;
		}
	}

	public static void Stop()
	{
		IsRunning = false;
		if (BotManager.Instance.chkLag.Checked)
		{
			LagKiller = false;
			SetLagKiller();
		}
		if (BotManager.Instance.chkProvoke.Checked)
		{
			ProvokeMonsters = false;
		}
		if (BotManager.Instance.chkProvokeAllMon.Checked)
		{
			ProvokeAllMonster = false;
		}
		if (BotManager.Instance.chkProvoke.Checked || BotManager.Instance.chkProvokeAllMon.Checked || Configuration.Instance.ProvokeMonsters || Configuration.Instance.ProvokeAllMonster || ProvokeMonsters || ProvokeAllMonster)
		{
			BotUtilities.MoveToSelfCell();
		}
		if (BotManager.Instance.chkSaveState.Checked)
		{
			_saveState = false;
		}
	}

	public static void SetClientLevel()
	{
		Proxy.Instance.SendToClient("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"levelUp\",\"intExpToLevel\":\"0\",\"intLevel\":" + Player.levelCap + "}}}");
	}

	public static void SetProvokeAllMonster()
	{
		if (!ProvokePacket.Contains("MonMapID"))
		{
			Proxy.Instance.SendToServer(ProvokePacket ?? "");
			return;
		}
		Proxy.Instance.SendToServer(string.Format("%xt%zm%aggroMon%{0}%{1}%", World.RoomId, string.Join("%", World.Monsters.ConvertAll((Monster m) => m.MonMapID))));
	}

	public static async void SetPacketSpam()
	{
		Proxy.Instance.SendToServer(SpamPacket ?? "");
		await Task.Delay(PacketDelay);
	}

	public static async void SaveState()
	{
		while (Player.IsLoggedIn && _saveState)
		{
			Proxy.Instance.SendToServer("%xt%zm%whisper%1%Save State: Ensures any gold, experience, reputation, and class points gained are saved. Triggers after every 5 minutes passed.%" + Player.Username + "%");
			LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss tt}] Save state has been triggered.");
			await Task.Delay(300000);
		}
	}

	public static async void AntiAfk()
	{
		if (_antiAfk)
		{
			while (Player.IsLoggedIn && _antiAfk)
			{
				Player.cancelAfk();
				await Task.Delay(60000);
			}
		}
	}

	private static void Provoke()
	{
		if (ProvokeMonsters)
		{
			World.SendPacket(string.Format("%xt%zm%aggroMon%{0}%{1}%", World.RoomId, string.Join("%", World.MonstersInCell(Player.Cell)?.ConvertAll((Monster m) => m.MonMapID))));
		}
		if (ProvokeAllMonster)
		{
			World.SendPacket((!ProvokePacket.Contains("MonMapID")) ? (ProvokePacket ?? "") : string.Format("%xt%zm%aggroMon%{0}%{1}%", World.RoomId, string.Join("%", World.Monsters?.ConvertAll((Monster m) => m.MonMapID))));
		}
	}

	public static async Task ApplySettings()
	{
		IsRunning = true;
		while (IsRunning && Player.IsLoggedIn)
		{
			if (PacketSpam && Player.IsAlive)
			{
				SetPacketSpam();
			}
			if (EnemyMagnet && Player.IsAlive)
			{
				SetEnemyMagnet();
			}
			if (Untarget && Player.targetOnSelf)
			{
				Player.CancelTargetSelf();
			}
			if (SkipCutscenes && World.ActiveCutscene > 0)
			{
				SetSkipCutscenes();
			}
			if (Player.walkSpeed != WalkSpeed)
			{
				SetWalkSpeed();
			}
			Provoke();
			await Task.Delay(Timer);
		}
	}

	static OptionsManager()
	{
		ProvokePacket = "%xt%zm%aggroMon%1%MonMapID%";
		Timer = 250;
		_antiAfk = true;
		empty = new string[0];
		HandlerHideRoom = new HandlerMapJoin();
		WalkSpeed = 8;
	}
}
