using System.Net;
using System.Net.Sockets;

namespace Grimoire.Utils;

public class NetworkUtils
{
	public static int GetAvailablePort()
	{
		using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
		return ((IPEndPoint)socket.LocalEndPoint).Port;
	}
}
