using UnityEngine;

namespace Infrastructure.Unity.Utils
{
    public static class Vector3Utils
    {
        // TODO: Test
        public static Vector3 WithX(this Vector3 vector3, float value)
        {
            return new Vector3(value, vector3.y, vector3.z);
        }

        // TODO: Test
        public static Vector3 WithY(this Vector3 vector3, float value)
        {
            return new Vector3(vector3.x, value, vector3.z);
        }

        // TODO: Test
        public static Vector3 WithZ(this Vector3 vector3, float value)
        {
            return new Vector3(vector3.x, vector3.y, value);
        }
    }
}