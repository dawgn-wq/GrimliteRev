using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdBeep : IBotCommand
{
	public int Times { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		for (int i = 0; i < Times; i++)
		{
			Console.Beep();
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return $"Beep {Times.ToString()} Times ";
	}
}
