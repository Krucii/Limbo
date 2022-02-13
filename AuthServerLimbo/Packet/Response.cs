using static AuthServerLimbo.Packet.PacketIDs;
using static AuthServerLimbo.Utils.ArrayUtils;
using System;

namespace AuthServerLimbo.Packet
{
    class Response
    {
        public static Packet ResponsePacket(Packet IncomingPacket)
        {
            switch ((PacketID)IncomingPacket.Id)
            {
                case PacketID.HANDSHAKE:
                    Console.WriteLine("Got handshake packet.");
                    return new Packet((byte)PacketID.HANDSHAKE, HandshakeResponse());
                case PacketID.PING:
                    Console.WriteLine("Got ping packet.");
                    return new Packet((byte)PacketID.PING, IncomingPacket.Data.ToArray());
                default:
                    return new Packet(0x0, new byte[] { 0 }); // empty packet, invalid
            }
        }

        private static byte[] HandshakeResponse()
        {
            string JSON = "{\"version\":{\"name\":\"1.8.8\",\"protocol\":47},\"players\":{\"max\":1,\"online\":0},\"description\":{\"text\":\"Hello world\"}}";
            return CreateArrayFromString(JSON, JSON.Length + 1, 1);
        }
    }
}
