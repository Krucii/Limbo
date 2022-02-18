using System;

namespace AuthServerLimbo.Packet.Server
{
    public class PlayerPositionAndLook : Packet, IPacket
    {
        public override byte Id => (byte)PacketIDs.ServerPacketId.PlayerPositionAndLook;

        public PlayerPositionAndLook()
        {
            Data.AddRange(BitConverter.GetBytes((double)0));
            Data.AddRange(BitConverter.GetBytes((double)0));
            Data.AddRange(BitConverter.GetBytes((double)0));
            Data.AddRange(BitConverter.GetBytes((float)0));
            Data.AddRange(BitConverter.GetBytes((float)0));
            Data.Add(0x00);
        }
    }
}
