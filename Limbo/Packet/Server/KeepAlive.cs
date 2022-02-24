using System;

namespace AuthServerLimbo.Packet.Server
{
    public class KeepAlive : Packet, IPacket
    {
        public override byte Id => (byte)PacketIDs.ServerPacketId.KeepAlive;
        private readonly Random _random = new();

        public KeepAlive()
        {
            Data.Add((byte)_random.Next(1, 20));
        }
    }
}
