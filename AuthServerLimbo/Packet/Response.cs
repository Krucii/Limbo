using static AuthServerLimbo.Packet.PacketIDs;
using static AuthServerLimbo.Utils.ArrayUtils;
using System;
using System.Linq;
using AuthServerLimbo.Packet.Server;

namespace AuthServerLimbo.Packet
{
    class Response
    {
        public static Packet ResponsePacket(Packet IncomingPacket)
        {
            Console.WriteLine(IncomingPacket.ToString());
            switch ((PacketID)IncomingPacket.Id)
            {
                // Server list ping
                case PacketID.HANDSHAKE when IncomingPacket.Data.Any() && (IncomingPacket.Data.Last() == 1 || IncomingPacket.Data.Last() == 2): //differs handshake and request
                    Console.WriteLine("Got handshake packet.");
                    return new Packet(); // empty packet, without sending
                case PacketID.HANDSHAKE when IncomingPacket.Data.Any() == false:
                    Console.WriteLine("Got request packet.");
                    return new Packet((byte)PacketID.HANDSHAKE, PingRequestResponse());
                case PacketID.PING:
                    Console.WriteLine("Got ping packet.");
                    return new Packet((byte)PacketID.PING, IncomingPacket.Data.ToArray());
                // Login sequence
                case PacketID.HANDSHAKE when IncomingPacket.Data.Any() && (IncomingPacket.Data.Last() != 1 || IncomingPacket.Data.Last() != 2):
                    var g = Guid.NewGuid().ToString();
                    byte[] Response = Combine(CreateArrayFromString(g, g.Length + 1, 1), IncomingPacket.Data.ToArray());
                    Packet p = new Packet((byte)PacketID.LOGIN, Response);
                    GVar.TEST = true;
                    return p;
                case PacketID.PLUGINMESSAGE:
                    Console.WriteLine("CHUJJJJ");
                    PlayerPositionAndLook ppal = new PlayerPositionAndLook();
                    return new Packet(ppal.ToByteArray());
                    
                default:
                    Console.WriteLine("Got invalid packet.");
                    return new Packet(); // empty packet, invalid
            }
        }

        private static byte[] PingRequestResponse()
        {
            string JSON = "{\"version\":{\"name\":\"1.8.8\",\"protocol\":47},\"players\":{\"max\":1,\"online\":0},\"description\":{\"text\":\"Hello world\"}}";
            return CreateArrayFromString(JSON, JSON.Length + 1, 1);
        }
    }
}
