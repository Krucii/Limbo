using System;
using System.Collections.Generic;

namespace AuthServerLimbo.Packet.Server.LoginSequence
{
    public class LoginSuccess : Packet, IPacket
    {
        public override byte Id => (byte)PacketIDs.ServerPacketId.LoginSuccess;
        
        private readonly PacketString _guid = new(Guid.NewGuid().ToString());

        public LoginSuccess(IEnumerable<byte> incomingPacket)
        {
            Data.AddRange(_guid.ToByteArray());
            Data.AddRange(incomingPacket);
        }
    }
}
