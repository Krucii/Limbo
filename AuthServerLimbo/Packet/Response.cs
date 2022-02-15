using static AuthServerLimbo.Packet.PacketIDs;
using static AuthServerLimbo.Utils.ArrayUtils;
using System;
using System.Linq;

namespace AuthServerLimbo.Packet
{
    class Response
    {
        public static Packet ResponsePacket(Packet IncomingPacket)
        {
            switch ((PacketID)IncomingPacket.Id)
            {
                case PacketID.HANDSHAKE when IncomingPacket.Data.Any(): //differs handshake and request
                    Console.WriteLine("Got handshake packet.");
                    return new Packet(); // empty packet, without sending
                case PacketID.HANDSHAKE when IncomingPacket.Data.Any() == false:
                    Console.WriteLine("Got request packet.");
                    return new Packet((byte)PacketID.HANDSHAKE, PingRequestResponse());
                case PacketID.PING:
                    Console.WriteLine("Got ping packet.");
                    return new Packet((byte)PacketID.PING, IncomingPacket.Data.ToArray());
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
