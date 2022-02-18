using System;
using System.Linq;
using static AuthServerLimbo.Packet.Response;
using static AuthServerLimbo.Packet.PacketIDs;
using System.Net;
using System.Net.Sockets;
using AuthServerLimbo.Packet;
using AuthServerLimbo.Packet.Server;
using AuthServerLimbo.Packet.Server.LoginSequence;

namespace AuthServerLimbo.Server
{
    internal class Server
    {
        private static readonly Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int BufferSize = 1024;
        private static readonly byte[] Buffer = new byte[BufferSize];

        public static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, 25565));
            ServerSocket.Listen(0);
            ServerSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }

        public static void CloseAllSockets()
        {
            ServerSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            Socket socket;

            try
            {
                socket = ServerSocket.EndAccept(ar);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            socket.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ReceiveCallback, socket);
            ServerSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            var current = (Socket)ar.AsyncState;
            int received;
            var closed = false;

            try
            {
                received = current!.EndReceive(ar);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current?.Close();
                return;
            }

            var receivedBuffer = new byte[received];
            Array.Copy(Buffer, receivedBuffer, received);
            if (receivedBuffer.Length > 0)
            {
                var id = receivedBuffer[1];
                var data = receivedBuffer.Skip(2).Take(receivedBuffer[0]).ToArray();
                var outgoing = ResponsePacket(id, data);
                if (outgoing.Length > 0) // checking, if packet has data in it
                {
                    current.Send(outgoing); // sending packet
                    if (id == (byte)ClientPacketId.Ping) //closing connection if packet was ping
                    {
                        current.Shutdown(SocketShutdown.Both);
                        current.Close();
                        closed = true;
                    }
                }
            }
            if (GVar.TEST)
            {
                var jg = new JoinGame();
                current.Send(jg.ToByteArray());
                var pm = new PluginMessage();
                current.Send(pm.ToByteArray());
                var sd = new ServerDifficulty();
                current.Send(sd.ToByteArray());
                var sp = new SpawnPosition();
                current.Send(sp.ToByteArray());
                var pa = new PlayerAbilities();
                current.Send(pa.ToByteArray());
                GVar.TEST = false;
            }
            

            if (!closed)
                current.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ReceiveCallback, current);
        }
    }
}
