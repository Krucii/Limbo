using System;
using System.Collections.Generic;

namespace AuthServerLimbo.Packet.Server
{
    public class ServerDifficulty : IPacket
    {
        public int Id => 0x41;
        
        public List<byte> Data = new();

        public ServerDifficulty()
        {
            Data.Add(0);
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
