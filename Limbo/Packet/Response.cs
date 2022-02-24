using static AuthServerLimbo.Packet.PacketIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using AuthServerLimbo.Client;
using AuthServerLimbo.Packet.Server.LoginSequence;
using AuthServerLimbo.Packet.Server.ServerListPing;

namespace AuthServerLimbo.Packet
{
    internal class Response
    {
        public static byte[] ResponsePacket(Client.Client client, byte id, IEnumerable<byte> data)
        {
            switch ((ClientPacketId)id)
            {
                // Server list ping
                case ClientPacketId.Handshake when client.GetState() == ClientState.None: //differs handshake and request
                    client.SetState((ClientState)data.Last());
                    return Array.Empty<byte>(); // empty packet, without sending
                case ClientPacketId.Request when client.GetState() == ClientState.Status:
                    var response = new Server.ServerListPing.Response();
                    return response.ToByteArray();
                case ClientPacketId.Ping when client.GetState() == ClientState.Status:
                    var pongResponse = new Pong(data.ToArray());
                    return pongResponse.ToByteArray();
                
                // Login sequence
                case ClientPacketId.Login when client.GetState() == ClientState.LoginInit:
                    var loginSuccess = new LoginSuccess(data.ToArray(), client);
                    client.SetState(ClientState.Login);
                    return loginSuccess.ToByteArray();
                case ClientPacketId.PluginMessage when client.GetState() == ClientState.Play:
                    var pluginMessageResponse = new PlayerPositionAndLook();
                    return pluginMessageResponse.ToByteArray();

                default:
                    return Array.Empty<byte>(); // empty packet, invalid
            }
        }
    }
}
