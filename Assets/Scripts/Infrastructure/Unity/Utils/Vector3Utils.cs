using System;
using UnityEngine;

namespace Infrastructure.Unity.Utils
{
    public static class Vector3Utils
    {
        public static Vector3 WithX(this Vector3 source, float value)
        {
            return new Vector3(value, source.y, source.z);
        }

        public static Vector3 WithY(this Vector3 source, float value)
        {
            return new Vector3(source.x, value, source.z);
        }

        public static Vector3 WithZ(this Vector3 source, float value)
        {
            return new Vector3(source.x, source.y, value);
        }

        public static Vector3 With(this Vector3 source, Vector3 value, Axis axis)
        {
            return
                new Vector3(
                    (axis & Axis.X) == Axis.X ? value.x : source.x,
                    (axis & Axis.Y) == Axis.Y ? value.y : source.y,
                    (axis & Axis.Z) == Axis.Z ? value.z : source.z
                );
        }

        public static Vector3 Abs(this Vector3 source)
        {
            return new Vector3(Math.Abs(source.x), Math.Abs(source.y), Math.Abs(source.z));
        }

        public static Vector3 Sign(this Vector3 source)
        {
            return new Vector3(Math.Sign(source.x), Math.Sign(source.y), Math.Sign(source.z));
        }

        public static Vector3 Remainder(this Vector3 source, float value)
        {
            return new Vector3(source.x % value, source.y % value, source.z % value);
        }

        public static Vector3 ClosestByCoordinate(this Vector3 source, Vector3 valueA, Vector3 valueB)
        {
            Vector3 distanceA = (valueA - source).Abs();
            Vector3 distanceB = (valueB - source).Abs();

            return
                new Vector3(
                    distanceA.x <= distanceB.x ? valueA.x : valueB.x,
                    distanceA.y <= distanceB.y ? valueA.y : valueB.y,
                    distanceA.z <= distanceB.z ? valueA.z : valueB.z
                );
        }
    }
}