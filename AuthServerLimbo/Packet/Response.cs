using static AuthServerLimbo.Packet.PacketIDs;
using System;
using System.Linq;
using AuthServerLimbo.Packet.Server.LoginSequence;
using AuthServerLimbo.Packet.Server.ServerListPing;

namespace AuthServerLimbo.Packet
{
    internal class Response
    {
        public static byte[] ResponsePacket(byte id, byte[] data)
        {
            switch ((ClientPacketId)id)
            {
                // Server list ping
                case ClientPacketId.Handshake when data.Any() && (data.Last() == 1 || data.Last() == 2): //differs handshake and request
                    return Array.Empty<byte>(); // empty packet, without sending
                case ClientPacketId.Request when data.Any() == false:
                    var response = new Server.ServerListPing.Response();
                    return response.ToByteArray();
                case ClientPacketId.Ping:
                    var pongResponse = new Pong(data.ToArray());
                    return pongResponse.ToByteArray();
                // Login sequence
                case ClientPacketId.Login when data.Any() && (data.Last() != 1 || data.Last() != 2):
                    var loginSuccess = new LoginSuccess(data.ToArray());
                    GVar.TEST = true;
                    return loginSuccess.ToByteArray();
                case ClientPacketId.PluginMessage:
                    var pluginMessageResponse = new PlayerPositionAndLook();
                    return pluginMessageResponse.ToByteArray();

                default:
                    return Array.Empty<byte>(); // empty packet, invalid
            }
        }
    }
}
