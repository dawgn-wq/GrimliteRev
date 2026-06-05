using System;
using System.Drawing;
using System.Windows.Forms;

namespace Grimoire.UI;

public class ResizableUserControl : UserControl
{
	private const int HTLEFT = 10;

	private const int HTRIGHT = 11;

	private const int HTTOP = 12;

	private const int HTTOPLEFT = 13;

	private const int HTTOPRIGHT = 14;

	private const int HTBOTTOM = 15;

	private const int HTBOTTOMLEFT = 16;

	private const int HTBOTTOMRIGHT = 17;

	private const int _ = 6;

	private new Rectangle Top => new Rectangle(0, 0, base.ClientSize.Width, 6);

	private new Rectangle Left => new Rectangle(0, 0, 6, base.ClientSize.Height);

	private new Rectangle Bottom => new Rectangle(0, base.ClientSize.Height - 6, base.ClientSize.Width, 6);

	private new Rectangle Right => new Rectangle(base.ClientSize.Width - 6, 0, 6, base.ClientSize.Height);

	private Rectangle TopLeft => new Rectangle(0, 0, 6, 6);

	private Rectangle TopRight => new Rectangle(base.ClientSize.Width - 6, 0, 6, 6);

	private Rectangle BottomLeft => new Rectangle(0, base.ClientSize.Height - 6, 6, 6);

	private Rectangle BottomRight => new Rectangle(base.ClientSize.Width - 6, base.ClientSize.Height - 6, 6, 6);

	public ResizableUserControl()
	{
		DoubleBuffered = true;
		SetStyle(ControlStyles.ResizeRedraw, value: true);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.FillRectangle(Brushes.Transparent, Top);
		e.Graphics.FillRectangle(Brushes.Transparent, Left);
		e.Graphics.FillRectangle(Brushes.Transparent, Right);
		e.Graphics.FillRectangle(Brushes.Transparent, Bottom);
	}

	protected override void WndProc(ref Message message)
	{
		base.WndProc(ref message);
		if (message.Msg == 132)
		{
			Point pt = PointToClient(Cursor.Position);
			if (TopLeft.Contains(pt))
			{
				message.Result = (IntPtr)13;
			}
			else if (TopRight.Contains(pt))
			{
				message.Result = (IntPtr)14;
			}
			else if (BottomLeft.Contains(pt))
			{
				message.Result = (IntPtr)16;
			}
			else if (BottomRight.Contains(pt))
			{
				message.Result = (IntPtr)17;
			}
			else if (Top.Contains(pt))
			{
				message.Result = (IntPtr)12;
			}
			else if (Left.Contains(pt))
			{
				message.Result = (IntPtr)10;
			}
			else if (Right.Contains(pt))
			{
				message.Result = (IntPtr)11;
			}
			else if (Bottom.Contains(pt))
			{
				message.Result = (IntPtr)15;
			}
		}
	}
}
