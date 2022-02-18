namespace AuthServerLimbo.Packet
{
    internal class PacketIDs
    {
        public enum ClientPacketId : byte
        {
            Handshake = 0x00, // can be request/response/login
            Ping = 0x01,
            PluginMessage = 0x17
        }

        public enum ServerPacketId : byte
        {
            JoinGame = 0x01,
            Login = 0x02,
            SpawnPosition = 0x05,
            PlayerPositionAndLook = 0x08, 
            PlayerAbilities = 0x39,
            PluginMessage = 0x3F,
            ServerDifficulty = 0x41
        }
    }
}
