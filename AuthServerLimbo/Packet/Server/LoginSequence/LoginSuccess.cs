using System;
using System.Text;

namespace AuthServerLimbo.Packet.Server.LoginSequence
{
    public class LoginSuccess : Packet, IPacket
    {
        public override byte Id => (byte)PacketIDs.ServerPacketId.LoginSuccess;
        
        private readonly PacketString _guid = new(Guid.NewGuid().ToString());

        public LoginSuccess(byte[] incomingPacket, Client.Client client)
        {
            Data.AddRange(_guid.ToByteArray());
            Data.AddRange(incomingPacket);
            // adding to client class
            client.SetUuid(_guid.GetText());
            client.SetUsername(Encoding.UTF8.GetString(incomingPacket, 1, incomingPacket[0]));
        }
    }
}
