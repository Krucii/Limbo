using System;
using System.Collections.Generic;

namespace AuthServerLimbo.Packet
{
    public abstract class Packet
    {
        protected readonly List<byte> Data;
        public abstract byte Id { get; }

        protected Packet()
        {
            Data = new List<byte>();
        }
        
        public byte[] ToByteArray()
        {
            var array = new byte[Data.Count + 2];
            array[0] = Convert.ToByte(Data.Count + 1);
            array[1] = Convert.ToByte(Id);
            var index = 2;
            foreach (var i in Data.ToArray())
            {
                array[index] = i;
                index++;
            }
            return array;
        }
    }
}
