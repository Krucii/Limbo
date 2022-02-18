using System;

namespace AuthServerLimbo.Packet.Server
{
    public class JoinGame : Packet, IPacket
    {
        public override byte Id => (int)PacketIDs.ServerPacketId.JoinGame;
        
        private readonly Random _random = new();
        private readonly PacketString _packetString = new("default");

        public JoinGame()
        {
            Data.AddRange(BitConverter.GetBytes(_random.Next(1, 100)));
            Data.Add(0);
            Data.Add(1);
            Data.Add(0);
            Data.Add(1);
            Data.AddRange(_packetString.ToByteArray());
            Data.Add(1);
        }
    }
}
