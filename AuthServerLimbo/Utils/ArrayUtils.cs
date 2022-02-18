using System;
using System.Linq;
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
        
        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays) {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
    }
}
