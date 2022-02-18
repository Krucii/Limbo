using System;
using System.Collections.Generic;

namespace AuthServerLimbo.Packet.Server
{
    public class JoinGame : IPacket
    {
        public int Id => 0x01;
        
        private Random r = new Random();
        
        public List<byte> Data = new();
        
        public PacketString a = new PacketString("default");

        public JoinGame()
        {
            Data.AddRange(BitConverter.GetBytes(r.Next(1, 100)));
            Data.Add(0);
            Data.Add(1);
            Data.Add(0);
            Data.Add(1);
            Data.AddRange(a.ToByteArray());
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
