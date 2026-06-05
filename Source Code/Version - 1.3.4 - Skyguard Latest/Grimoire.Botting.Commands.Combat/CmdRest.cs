using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Combat;

public class CmdRest : IBotCommand
{
	public bool Full { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Rest;
		await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Rest), () => instance.IsRunning && Player.IsLoggedIn, 10, 500);
		if (instance.Configuration.ExitCombatBeforeRest)
		{
			await BotUtilities.MoveOutOfCombat();
		}
		Player.Rest();
		if (Full)
		{
			await instance.WaitUntil(() => Player.Mana >= Player.ManaMax && Player.Health >= Player.HealthMax, null, 30, 500);
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		if (!Full)
		{
			return "Rest";
		}
		return "Rest fully";
	}
}
