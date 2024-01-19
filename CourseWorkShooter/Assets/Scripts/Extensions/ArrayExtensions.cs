using System;

namespace Extensions
{
    public static class ArrayExtensions
    {
        public static bool IsEmpty(this Array array)
        {
            return array == null || array.Length == 0;
        }
    }
}