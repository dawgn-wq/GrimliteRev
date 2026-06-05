using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;
using Grimoire.UI;
using Newtonsoft.Json;
using Properties;

namespace Grimoire.Botting.Commands.Misc;

public class CmdLoadBot : IBotCommand
{
	public string BotFileName { get; set; }

	public string BotFilePath { get; set; }

	private string _path => BotManager.Instance.ActiveBotEngine.Value(BotFilePath);

	public async Task Execute(IBotEngine instance)
	{
		if (!File.Exists(_path))
		{
			return;
		}
		try
		{
			string value;
			using (TextReader reader = new StreamReader(_path))
			{
				value = await reader.ReadToEndAsync();
			}
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.Include,
				TypeNameHandling = TypeNameHandling.All
			};
			Configuration configuration = JsonConvert.DeserializeObject<Configuration>(value, settings);
			int currentConfiguration = instance.CurrentConfiguration;
			if (configuration != null && configuration.Commands.Count > 0)
			{
				if (!Bot.Configurations.ContainsKey(currentConfiguration))
				{
					Bot.Configurations.Add(currentConfiguration, instance.Configuration);
				}
				else
				{
					Bot.Configurations[currentConfiguration] = instance.Configuration;
				}
				if (!Bot.OldIndex.ContainsKey(currentConfiguration))
				{
					Bot.OldIndex.Add(currentConfiguration, instance.Index);
				}
				else
				{
					Bot.OldIndex[currentConfiguration] = instance.Index;
				}
				instance.Configuration = configuration;
				instance.Index = -1;
				instance.CurrentConfiguration++;
				BotManager.Instance.LastIndexedSearch = 0;
				await instance.LoadBotQuests();
			}
		}
		catch (Exception ex)
		{
			string message = ex.Message;
			DarkMessageBox.Show(new Form
			{
				TopMost = true,
				StartPosition = FormStartPosition.CenterScreen,
				Icon = Resources.GrimoireIcon
			}, "Failed to load the bot. You cannot load any bot that has commands not available in this client.\r\n\r\nError Message:\r\n" + message, "Load Bot", MessageBoxIcon.Hand);
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Load bot: " + BotFileName;
	}
}
