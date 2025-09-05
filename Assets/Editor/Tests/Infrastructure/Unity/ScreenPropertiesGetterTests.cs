using Infrastructure.Unity;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Unity
{
    public class ScreenPropertiesGetterTests
    {
        private ScreenPropertiesGetter _screenPropertiesGetter;

        [SetUp]
        public void SetUp()
        {
            _screenPropertiesGetter = new ScreenPropertiesGetter();
        }

        [Test]
        public void Width_ReturnsScreenWidth()
        {
            int expectedResult = Screen.width;

            int result = _screenPropertiesGetter.Width;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Height_ReturnsScreenHeight()
        {
            int expectedResult = Screen.height;

            int result = _screenPropertiesGetter.Height;

            Assert.AreEqual(expectedResult, result);
        }
    }
}