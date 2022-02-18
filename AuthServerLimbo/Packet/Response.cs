using static AuthServerLimbo.Packet.PacketIDs;
using static AuthServerLimbo.Utils.ArrayUtils;
using System;
using System.Linq;
using AuthServerLimbo.Packet.Server;

namespace AuthServerLimbo.Packet
{
    internal class Response
    {
        public static PacketOld ResponsePacket(PacketOld incomingPacketOld)
        {
            //Console.WriteLine(incomingPacketOld.ToString());
            switch ((ClientPacketId)incomingPacketOld.Id)
            {
                // Server list ping
                case ClientPacketId.Handshake when incomingPacketOld.Data.Any() && (incomingPacketOld.Data.Last() == 1 || incomingPacketOld.Data.Last() == 2): //differs handshake and request
                    return new PacketOld(); // empty packet, without sending
                case ClientPacketId.Handshake when incomingPacketOld.Data.Any() == false:
                    return new PacketOld((byte)ClientPacketId.Handshake, PingRequestResponse());
                case ClientPacketId.Ping:
                    return new PacketOld((byte)ClientPacketId.Ping, incomingPacketOld.Data.ToArray());
                // Login sequence
                case ClientPacketId.Handshake when incomingPacketOld.Data.Any() && (incomingPacketOld.Data.Last() != 1 || incomingPacketOld.Data.Last() != 2):
                    Console.WriteLine("New login");
                    var g = Guid.NewGuid().ToString();
                    byte[] Response = Combine(CreateArrayFromString(g, g.Length + 1, 1), incomingPacketOld.Data.ToArray());
                    var p = new PacketOld((byte)ServerPacketId.Login, Response);
                    GVar.TEST = true;
                    return p;
                case ClientPacketId.PluginMessage:
                    PlayerPositionAndLook ppal = new PlayerPositionAndLook();
                    return new PacketOld(ppal.ToByteArray());
                    
                default:
                    return new PacketOld(); // empty packet, invalid
            }
        }

        private static byte[] PingRequestResponse()
        {
            string JSON = "{\"version\":{\"name\":\"1.8.8\",\"protocol\":47},\"players\":{\"max\":1,\"online\":0},\"description\":{\"text\":\"Hello world\"}}";
            return CreateArrayFromString(JSON, JSON.Length + 1, 1);
        }
    }
}
