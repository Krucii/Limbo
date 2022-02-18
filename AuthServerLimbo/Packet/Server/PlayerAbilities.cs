using System;

namespace AuthServerLimbo.Packet.Server
{
    public class PlayerAbilities : Packet, IPacket
    {
        public override byte Id => (byte)PacketIDs.ServerPacketId.PlayerAbilities;
        
        public PlayerAbilities()
        {
            Data.Add(0x01);
            Data.AddRange(BitConverter.GetBytes((float)0));
            Data.AddRange(BitConverter.GetBytes((float)0));
        }
    }
}
