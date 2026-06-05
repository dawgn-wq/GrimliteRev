using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdFollow : RegularExpression, IBotCommand
{
	public string Name { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		if (!instance.IsVar(Name))
		{
			_ = Name;
		}
		else
		{
			_ = Configuration.Tempvariable[instance.GetVar(Name)];
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Follow player: " + Name;
	}
}
