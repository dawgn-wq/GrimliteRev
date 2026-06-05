using System.Threading.Tasks;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Item;

public class CmdWhitelist : IBotCommand
{
	public enum state
	{
		On,
		Off,
		Clear,
		Add,
		Remove
	}

	public string Item { get; set; }

	public state State { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		switch (State)
		{
		case state.On:
			instance.Configuration.EnablePickup = true;
			BotManager.Instance.chkPickup.Checked = true;
			break;
		case state.Off:
			instance.Configuration.EnablePickup = false;
			BotManager.Instance.chkPickup.Checked = false;
			break;
		case state.Clear:
			BotManager.Instance.lstDrops.Items.Clear();
			instance.Configuration.Drops.Clear();
			break;
		case state.Add:
			BotManager.Instance.AddDrop(instance.Value(Item), toConfig: true);
			break;
		case state.Remove:
			BotManager.Instance.RemoveDrop(instance.Value(Item), toConfig: true);
			break;
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return State switch
		{
			state.On => "Drop list: On", 
			state.Off => "Drop list: Off", 
			state.Add => "Add to Drop list: " + Item, 
			state.Remove => "Remove from Drop list: " + Item, 
			state.Clear => "Clear Drop list", 
			_ => "Drop list", 
		};
	}
}
