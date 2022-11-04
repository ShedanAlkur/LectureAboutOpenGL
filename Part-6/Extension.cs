using System;
using System.Collections.Generic;
using System.Text;

namespace Part_6
{
    static class Extension
    {
        /// <summary>
        /// Returns the number of bytes in the specified array.
        /// </summary>
        /// <param name="array">An array.</param>
        /// <returns>The number of bytes in array.</returns>
        public static int Size(this Array array)
        {
            return Buffer.ByteLength(array);
        } 
    }
}
