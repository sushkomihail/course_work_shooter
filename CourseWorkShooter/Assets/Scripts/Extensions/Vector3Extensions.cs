using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        public static bool IsComparableWith(this Vector3 a, Vector3 b)
        {
            return (a - b).magnitude < 0.05f;
        }
    }
}