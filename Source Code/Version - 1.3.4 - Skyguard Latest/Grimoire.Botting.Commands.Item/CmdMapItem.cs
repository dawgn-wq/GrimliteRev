using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Item;

public class CmdMapItem : IBotCommand
{
	public int ItemId { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Others;
		await instance.WaitUntil(() => World.IsActionAvailable(LockActions.GetMapItem));
		Player.GetMapItem(ItemId);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return $"Get map item: {ItemId}";
	}
}
