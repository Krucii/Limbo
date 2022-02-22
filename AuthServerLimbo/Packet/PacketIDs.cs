namespace AuthServerLimbo.Packet
{
    internal class PacketIDs
    {
        public enum ClientPacketId : byte
        {
            Handshake, Request = 0x00,
            Login = 0x00,
            Ping = 0x01,
            PluginMessage = 0x17
        }

        public enum ServerPacketId : byte
        {
            Response, KeepAlive = 0x00,
            JoinGame, Pong = 0x01,
            LoginSuccess = 0x02,
            SpawnPosition = 0x05,
            PlayerPositionAndLook = 0x08, 
            PlayerAbilities = 0x39,
            PluginMessage = 0x3F,
            ServerDifficulty = 0x41
        }
    }
}
