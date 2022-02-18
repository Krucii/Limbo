using System;

namespace AuthServerLimbo.Packet.Server.LoginSequence
{
    public class SpawnPosition : Packet, IPacket
    {
        public override byte Id => (byte)PacketIDs.ServerPacketId.SpawnPosition;

        public SpawnPosition()
        {
            Data.AddRange(BitConverter.GetBytes((long)0));
        }
    }
}
