using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdLabel : IBotCommand
{
	public string Name { get; set; }

	public Task Execute(IBotEngine instance)
	{
		return Task.FromResult<object>(null);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "[" + Name.ToUpper() + "]";
	}
}
