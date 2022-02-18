namespace AuthServerLimbo.Packet.Server.ServerListPing
{
    public class Response : Packet, IPacket
    {
        public override byte Id => (byte)PacketIDs.ServerPacketId.Response;
        
        private readonly PacketString _packetString = new("{\"version\":{\"name\":\"1.8.8\",\"protocol\":47},\"players\":{\"max\":1,\"online\":0},\"description\":{\"text\":\"Hello world\"}}");

        public Response()
        {
            Data.AddRange(_packetString.ToByteArray());
        }
    }
}
