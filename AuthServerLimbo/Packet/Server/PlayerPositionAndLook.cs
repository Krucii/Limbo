using System;
using System.Collections.Generic;

namespace AuthServerLimbo.Packet.Server
{
    public class PlayerPositionAndLook : IPacket
    {
        public int Id => 0x08;
        public List<byte> Data = new();

        public PlayerPositionAndLook()
        {
            Data.AddRange(BitConverter.GetBytes((double)0));
            Data.AddRange(BitConverter.GetBytes((double)0));
            Data.AddRange(BitConverter.GetBytes((double)0));
            Data.AddRange(BitConverter.GetBytes((float)0));
            Data.AddRange(BitConverter.GetBytes((float)0));
            Data.Add(0x00);
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
