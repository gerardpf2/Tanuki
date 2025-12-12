using Infrastructure.Unity;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Unity
{
    public class CameraGetterTests
    {
        private CameraGetter _cameraGetter;

        [SetUp]
        public void SetUp()
        {
            _cameraGetter = new CameraGetter();
        }

        [Test]
        public void GetMain_ReturnsCameraMain()
        {
            Camera expectedResult = Camera.main;

            Camera result = _cameraGetter.GetMain();

            Assert.AreSame(expectedResult, result);
        }
    }
}