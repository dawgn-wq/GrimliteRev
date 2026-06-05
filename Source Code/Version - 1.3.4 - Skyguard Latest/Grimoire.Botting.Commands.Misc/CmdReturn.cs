using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdReturn : IBotCommand
{
	public async Task Execute(IBotEngine instance)
	{
		try
		{
			int key = --instance.CurrentConfiguration;
			Configuration configuration = Bot.Configurations[key];
			int num = Bot.OldIndex[key];
			if (configuration != null && configuration.Commands.Count > 0 && num > -1)
			{
				instance.Configuration = configuration;
				instance.Index = num;
				await instance.LoadBotQuests();
			}
		}
		catch
		{
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Return";
	}
}
