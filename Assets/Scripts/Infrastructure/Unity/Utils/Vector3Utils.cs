using UnityEngine;

namespace Infrastructure.Unity.Utils
{
    public static class Vector3Utils
    {
        // TODO: Test
        public static Vector3 WithX(this Vector3 source, float value)
        {
            return new Vector3(value, source.y, source.z);
        }

        // TODO: Test
        public static Vector3 WithY(this Vector3 source, float value)
        {
            return new Vector3(source.x, value, source.z);
        }

        // TODO: Test
        public static Vector3 WithZ(this Vector3 source, float value)
        {
            return new Vector3(source.x, source.y, value);
        }

        // TODO: Test
        public static Vector3 With(this Vector3 source, Vector3 target, Axis axis)
        {
            return
                new Vector3(
                    (axis & Axis.X) == Axis.X ? target.x : source.x,
                    (axis & Axis.Y) == Axis.Y ? target.y : source.y,
                    (axis & Axis.Z) == Axis.Z ? target.z : source.z
                );
        }
    }
}