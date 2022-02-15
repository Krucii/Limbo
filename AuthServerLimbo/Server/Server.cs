using System;
using static AuthServerLimbo.Packet.Response;
using static AuthServerLimbo.Packet.PacketIDs;
using System.Net;
using System.Net.Sockets;

namespace AuthServerLimbo.Server
{
    class Server
    {
        private static readonly Socket _ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly int _BufferSize = 1024;
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

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = _ServerSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            socket.BeginReceive(_Buffer, 0, _BufferSize, SocketFlags.None, ReceiveCallback, socket);
            _ServerSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;
            bool closed = false;

            try
            {
                received = current.EndReceive(AR);
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
                /* Console.WriteLine("RECEIVED");
                Console.WriteLine("Length: " + p.length);
                Console.WriteLine("ID: {0:X}", p.id);
                Console.Write("Data: ");
                foreach (var b in p.data)
                {
                    Console.Write("{0:X} ", b);
                }
                Console.WriteLine(); */
                Packet.Packet outgoing = ResponsePacket(p);
                if (!outgoing.IsEmpty())
                {
                    current.Send(outgoing.PacketBuilder());
                    if (p.Id == (byte)PacketID.PING)
                    {
                        current.Close();
                        closed = true;
                    }
                }
            }

            if (closed == false)
                current.BeginReceive(_Buffer, 0, _BufferSize, SocketFlags.None, ReceiveCallback, current);
        }
    }
}
