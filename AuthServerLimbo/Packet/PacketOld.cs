using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthServerLimbo.Packet
{
    public class PacketOld
    {
        private byte Length { get; set; }
        public byte Id { get; set; }
        public List<byte> Data = new();

        private void SetLength()
        {
            Length = Convert.ToByte(Data.Count + 1);
        }

        public byte[] PacketBuilder()
        {
            byte[] b = new byte[Length + 1];
            b[0] = Length;
            b[1] = Id;
            int index = 2;
            foreach (var i in Data.ToArray())
            {
                b[index] = i;
                index++;
            }
            return b;
        }

        public PacketOld(byte PacketID, byte[] content)
        {
            Id = PacketID;
            Data = content.ToList();
            SetLength();
        }

        public PacketOld(byte[] p)
        {
            Length = p[0];
            Id = p[1];
            Data.AddRange(p.Skip(2).Take(Length));
        }

        public PacketOld()
        {
            Length = 0;
        }

        public bool IsEmpty()
        {
            return Length == 0 ? true : false;
        }

        public override string ToString()
        {
            return $@"[Len: {Length}] [ID: {Id}] [Data: {String.Join(" ", Data)}]";
        }
    }
}
