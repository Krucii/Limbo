using System;
using static AuthServerLimbo.Packet.Response;
using static AuthServerLimbo.Packet.PacketIDs;
using System.Net;
using System.Net.Sockets;
using AuthServerLimbo.Packet;
using AuthServerLimbo.Packet.Server;

namespace AuthServerLimbo.Server
{
    class Server
    {
        private static readonly Socket _ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int _BufferSize = 1024;
        private static readonly byte[] _Buffer = new byte[_BufferSize];

        public static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            _ServerSocket.Bind(new IPEndPoint(IPAddress.Any, 25565));
            _ServerSocket.Listen(0);
            _ServerSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }

        public static void CloseAllSockets()
        {
            _ServerSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            Socket socket;

            try
            {
                socket = _ServerSocket.EndAccept(ar);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            socket.BeginReceive(_Buffer, 0, _BufferSize, SocketFlags.None, ReceiveCallback, socket);
            _ServerSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            Socket current = (Socket)ar.AsyncState;
            int received;
            bool closed = false;

            try
            {
                received = current.EndReceive(ar);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                return;
            }

            byte[] receivedBuffer = new byte[received];
            Array.Copy(_Buffer, receivedBuffer, received);
            if (receivedBuffer.Length > 0)
            {
                var p = new Packet.Packet(receivedBuffer);
                
                Packet.Packet outgoing = ResponsePacket(p);
                if (!outgoing.IsEmpty()) // checking, if packet has data in it
                {
                    current.Send(outgoing.PacketBuilder()); // sending packet
                    if (p.Id == (byte)PacketID.PING) //closing connection if packet was ping
                    {
                        current.Shutdown(SocketShutdown.Both);
                        current.Close();
                        closed = true;
                    }
                    if (p.Id == 23)
                    {
                        Console.WriteLine("kurwa");
                        PlayerPositionAndLook ppal = new PlayerPositionAndLook();
                        current.Send(ppal.ToByteArray());
                    }
                }
            }
            if (GVar.TEST == true)
            {
                JoinGame jg = new JoinGame();
                current.Send(jg.ToByteArray());
                PluginMessage pm = new PluginMessage();
                current.Send(pm.ToByteArray());
                ServerDifficulty sd = new ServerDifficulty();
                current.Send(sd.ToByteArray());
                SpawnPosition sp = new SpawnPosition();
                current.Send(sp.ToByteArray());
                PlayerAbilities pa = new PlayerAbilities();
                current.Send(pa.ToByteArray());
                GVar.TEST = false;
            }
            

            if (closed == false)
                current.BeginReceive(_Buffer, 0, _BufferSize, SocketFlags.None, ReceiveCallback, current);
        }
    }
}
