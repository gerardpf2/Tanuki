using System;
using Infrastructure.ScreenLoading;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.ScreenLoading
{
    public class ScreenPlacementContainerTests
    {
        private IScreenPlacement _screenPlacement;

        private ScreenPlacementContainer _screenPlacementContainer;

        [SetUp]
        public void SetUp()
        {
            _screenPlacement = Substitute.For<IScreenPlacement>();

            _screenPlacementContainer = new ScreenPlacementContainer();
        }

        [Test]
        public void Add_KeyNull_ThrowsException()
        {
            const string key = null;
            _screenPlacement.Key.Returns(key);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _screenPlacementContainer.Add(_screenPlacement));
            Assert.AreEqual($"Cannot add screen placement with Key: {key}", invalidOperationException.Message);
        }

        [Test]
        public void Add_Duplicated_ThrowsException()
        {
            const string key = nameof(key);
            _screenPlacement.Key.Returns(key);
            _screenPlacementContainer.Add(_screenPlacement);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _screenPlacementContainer.Add(_screenPlacement));
            Assert.AreEqual($"Cannot add screen placement with Key: {key}", invalidOperationException.Message);
        }

        [Test]
        public void Remove_KeyNull_ThrowsException()
        {
            const string key = null;
            _screenPlacement.Key.Returns(key);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _screenPlacementContainer.Remove(_screenPlacement));
            Assert.AreEqual($"Cannot remove screen placement with Key: {key}", invalidOperationException.Message);
        }

        [Test]
        public void Remove_NotAdded_ThrowsException()
        {
            const string key = nameof(key);
            _screenPlacement.Key.Returns(key);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _screenPlacementContainer.Remove(_screenPlacement));
            Assert.AreEqual($"Cannot remove screen placement with Key: {key}", invalidOperationException.Message);
        }

        [Test]
        public void Get_Added_ReturnsExpected()
        {
            const string key = nameof(key);
            _screenPlacement.Key.Returns(key);
            _screenPlacementContainer.Add(_screenPlacement);

            IScreenPlacement screenPlacement = _screenPlacementContainer.Get(key);

            Assert.AreSame(_screenPlacement, screenPlacement);
        }

        [Test]
        public void Get_NotAdded_ThrowsException()
        {
            const string key = nameof(key);
            _screenPlacement.Key.Returns(key);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => { IScreenPlacement _ = _screenPlacementContainer.Get(key); });
            Assert.AreEqual($"Cannot get screen placement with Key: {key}", invalidOperationException.Message);
        }
    }
}