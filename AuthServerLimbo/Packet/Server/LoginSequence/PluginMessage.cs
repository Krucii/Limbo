using System.Text;

namespace AuthServerLimbo.Packet.Server.LoginSequence
{
    public class PluginMessage : Packet, IPacket
    {
        public override byte Id => (byte)PacketIDs.ServerPacketId.PluginMessage;

        private readonly PacketString _packetString = new("MC|Brand");

        public PluginMessage()
        {
            Data.AddRange(_packetString.ToByteArray());
            Data.AddRange(Encoding.UTF8.GetBytes("vanilla"));
        }
    }
}
