using System;
using System.Text;
using Microsoft.VisualBasic;

namespace AuthServerLimbo.Packet
{
    public class PacketString
    {
        private int Length { get; }
        private string Data { get; }

        public PacketString(string text)
        {
            Length = text.Length;
            Data = text;
        }

        public byte[] ToByteArray()
        {
            byte[] array = new byte[Length + 1];
            array[0] = Convert.ToByte(Length); // strings must be prefixed with its length
            int index = 1;
            foreach (var b in Encoding.UTF8.GetBytes(Data))
            {
                array[index] = b;
                index++;
            }
            return array;
        }

        public int GetLength() => Length;
        public string GetText() => Data;
    }
}
