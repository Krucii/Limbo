namespace AuthServerLimbo.Packet.Server
{
    public class ServerDifficulty : Packet, IPacket
    {
        public override byte Id => (byte)PacketIDs.ServerPacketId.ServerDifficulty;

        public ServerDifficulty()
        {
            Data.Add(0);
        }
    }
}
