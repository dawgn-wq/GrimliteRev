using Grimoire.Botting;
using Grimoire.Game;

namespace Grimoire.Networking.Handlers;

public class HandlerXtCellJoin : IXtMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "moveToCell" };

	public void Handle(XtMessage message)
	{
		World.SetSpawnPoint();
		if (OptionsManager.HideYulgar && Player.Map.ToLower() == "yulgar")
		{
			OptionsManager.DestroyPlayers(OptionsManager.HideYulgar);
		}
		if (OptionsManager.HidePlayers)
		{
			OptionsManager.DestroyPlayers(Enabled: true);
		}
	}

	void IXtMessageHandler.Handle(XtMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}
}
