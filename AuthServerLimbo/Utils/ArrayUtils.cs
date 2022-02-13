using System;
using System.Text;

namespace AuthServerLimbo.Utils
{
    class ArrayUtils
    {
        public static byte[] CreateArrayFromString(string str, int length, int offset)
        {
            int index = offset;
            byte[] array = new byte[length];
            array[0] = Convert.ToByte(str.Length); // strings must be prefixed with its length
            foreach (var b in Encoding.UTF8.GetBytes(str))
            {
                array[index] = b;
                index++;
            }
            return array;
        }
    }
}
