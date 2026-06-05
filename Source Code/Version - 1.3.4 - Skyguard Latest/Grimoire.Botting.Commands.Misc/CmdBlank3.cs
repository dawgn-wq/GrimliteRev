using System.Drawing;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdBlank3 : IBotCommand
{
	public string Text { get; set; }

	public int Alpha { get; set; }

	public int R { get; set; }

	public int G { get; set; }

	public int B { get; set; }

	public async Task Execute(IBotEngine instance)
	{
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public Color Argb()
	{
		return Color.FromArgb(Alpha, R, G, B);
	}

	public override string ToString()
	{
		return Text ?? "";
	}
}
