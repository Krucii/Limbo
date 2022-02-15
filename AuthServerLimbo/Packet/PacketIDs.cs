namespace AuthServerLimbo.Packet
{
    class PacketIDs
    {
        public enum PacketID : byte
        {
            HANDSHAKE, // can be request/response
            PING
        }
    }
}
