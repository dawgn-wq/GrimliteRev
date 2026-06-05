using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc;

public class CmdLoadMapSwf : IBotCommand
{
	public string Name { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer), null, 10, 500);
		Player.LoadMap(instance.IsVar(Name) ? Configuration.Tempvariable[instance.GetVar(Name)] : Name);
		await instance.WaitUntil(() => !World.IsMapLoading, null, 10, 500);
		if (AutoRelogin.IsClientLoading("MapLoadingStuck") || AutoRelogin.IsClientLoading("MapLoadingError"))
		{
			World.ReloadCurrentMap();
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Load Map SWF: " + Name;
	}
}
