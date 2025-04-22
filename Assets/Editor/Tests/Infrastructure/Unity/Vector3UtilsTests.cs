using System;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Unity
{
    public class Vector3UtilsTests
    {
        [Test]
        public void WithX_ReturnsExpected()
        {
            Vector3 source = new(0.0f, 1.0f, 2.0f);
            const float value = 3.0f;

            Vector3 result = source.WithX(value);

            Assert.AreEqual(value, result.x);
            Assert.AreEqual(source.y, result.y);
            Assert.AreEqual(source.z, result.z);
        }

        [Test]
        public void WithY_ReturnsExpected()
        {
            Vector3 source = new(0.0f, 1.0f, 2.0f);
            const float value = 3.0f;

            Vector3 result = source.WithY(value);

            Assert.AreEqual(value, result.y);
            Assert.AreEqual(source.x, result.x);
            Assert.AreEqual(source.z, result.z);
        }

        [Test]
        public void WithZ_ReturnsExpected()
        {
            Vector3 source = new(0.0f, 1.0f, 2.0f);
            const float value = 3.0f;

            Vector3 result = source.WithZ(value);

            Assert.AreEqual(value, result.z);
            Assert.AreEqual(source.x, result.x);
            Assert.AreEqual(source.y, result.y);
        }

        [TestCase(0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, Axis.X, 3.0f, 1.0f, 2.0f)]
        [TestCase(0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, Axis.Y, 0.0f, 4.0f, 2.0f)]
        [TestCase(0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, Axis.Z, 0.0f, 1.0f, 5.0f)]
        [TestCase(0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, Axis.X | Axis.Y, 3.0f, 4.0f, 2.0f)]
        [TestCase(0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, Axis.X | Axis.Z, 3.0f, 1.0f, 5.0f)]
        [TestCase(0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, Axis.Y | Axis.Z, 0.0f, 4.0f, 5.0f)]
        [TestCase(0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, Axis.All, 3.0f, 4.0f, 5.0f)]
        public void With_ReturnsExpected(
            float sourceX,
            float sourceY,
            float sourceZ,
            float valueX,
            float valueY,
            float valueZ,
            Axis axis,
            float expectedResultX,
            float expectedResultY,
            float expectedResultZ)
        {
            Vector3 source = new(sourceX, sourceY, sourceZ);
            Vector3 value = new(valueX, valueY, valueZ);

            Vector3 result = source.With(value, axis);

            Assert.AreEqual(expectedResultX, result.x);
            Assert.AreEqual(expectedResultY, result.y);
            Assert.AreEqual(expectedResultZ, result.z);
        }

        [Test]
        public void Abs_ReturnsExpected()
        {
            Vector3 source = new(0.0f, -1.0f, 2.0f);

            Vector3 result = source.Abs();

            Assert.AreEqual(Math.Abs(source.x), result.x);
            Assert.AreEqual(Math.Abs(source.y), result.y);
            Assert.AreEqual(Math.Abs(source.z), result.z);
        }

        [Test]
        public void Sign_ReturnsExpected()
        {
            Vector3 source = new(0.0f, -1.0f, 2.0f);

            Vector3 result = source.Sign();

            Assert.AreEqual(Math.Sign(source.x), result.x);
            Assert.AreEqual(Math.Sign(source.y), result.y);
            Assert.AreEqual(Math.Sign(source.z), result.z);
        }

        [Test]
        public void Remainder_ReturnsExpected()
        {
            Vector3 source = new(0.0f, -1.0f, 2.0f);
            const float value = 2.0f;

            Vector3 result = source.Remainder(value);

            Assert.AreEqual(source.x % value, result.x);
            Assert.AreEqual(source.y % value, result.y);
            Assert.AreEqual(source.z % value, result.z);
        }

        [Test]
        public void ClosestByCoordinate_ReturnsExpected()
        {
            Vector3 source = new(0.0f, -1.0f, 2.0f);
            Vector3 valueA = new(-1.0f, -1.5f, 2000.0f);
            Vector3 valueB = new(2.0f, -1.25f, 2001.0f);

            Vector3 result = source.ClosestByCoordinate(valueA, valueB);

            Assert.AreEqual(valueA.x, result.x);
            Assert.AreEqual(valueB.y, result.y);
            Assert.AreEqual(valueA.z, result.z);
        }
    }
}