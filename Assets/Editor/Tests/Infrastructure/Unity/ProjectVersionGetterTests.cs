using Infrastructure.Unity;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Unity
{
    public class ProjectVersionGetterTests
    {
        private ProjectVersionGetter _projectVersionGetter;

        [SetUp]
        public void SetUp()
        {
            _projectVersionGetter = new ProjectVersionGetter();
        }

        [Test]
        public void Get_ReturnsApplicationVersion()
        {
            string expectedVersion = Application.version;

            string version = _projectVersionGetter.Get();

            Assert.AreEqual(expectedVersion, version);
        }
    }
}