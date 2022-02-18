using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServerLimbo.Packet.Server
{
    public class PluginMessage : IPacket
    {
        public int Id => 0x3F;
        
        public List<byte> Data = new();
        public PacketString a = new PacketString("MC|Brand");

        public PluginMessage()
        {
            Data.AddRange(a.ToByteArray());
            Data.AddRange(Encoding.UTF8.GetBytes("vanilla"));
        }

        public byte[] ToByteArray()
        {
            byte[] array = new byte[Data.Count + 2];
            array[0] = Convert.ToByte(Data.Count + 1);
            array[1] = Convert.ToByte(Id);
            int index = 2;
            foreach (var i in Data.ToArray())
            {
                array[index] = i;
                index++;
            }
            return array;
        }
    }
}
