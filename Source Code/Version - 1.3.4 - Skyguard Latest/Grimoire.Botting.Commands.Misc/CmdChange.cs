using System.Threading.Tasks;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Misc;

public class CmdChange : IBotCommand
{
	public bool Guild { get; set; }

	public string Text { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		if (Guild)
		{
			BotManager.Instance.CustomGuild = instance.Value(Text);
		}
		else
		{
			BotManager.Instance.CustomName = instance.Value(Text);
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		if (Guild)
		{
			return "Guild: " + Text;
		}
		return "Name: " + Text;
	}
}
