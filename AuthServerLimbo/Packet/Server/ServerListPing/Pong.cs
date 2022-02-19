using System.Collections.Generic;

namespace AuthServerLimbo.Packet.Server.ServerListPing
{
    public class Pong : Packet, IPacket
    {
        public override byte Id => (byte)PacketIDs.ServerPacketId.Pong;

        public Pong(IEnumerable<byte> incomingPacket)
        {
            Data.AddRange(incomingPacket);
        }
    }
}
