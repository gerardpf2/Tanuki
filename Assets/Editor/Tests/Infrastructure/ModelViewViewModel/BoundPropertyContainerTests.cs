using System;
using Infrastructure.ModelViewViewModel;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.ModelViewViewModel
{
    public class BoundPropertyContainerTests
    {
        private BoundPropertyContainer _boundPropertyContainer;

        [SetUp]
        public void SetUp()
        {
            _boundPropertyContainer = new BoundPropertyContainer();
        }

        [Test]
        public void Add_KeyNull_ThrowsException()
        {
            IBoundProperty<object> boundProperty = Substitute.For<IBoundProperty<object>>();
            const string key = null;
            boundProperty.Key.Returns(key);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _boundPropertyContainer.Add(boundProperty));
            Assert.AreEqual($"Cannot add bound property with Type: {typeof(object)} and Key: {key}", invalidOperationException.Message);
        }

        [Test]
        public void Add_KeyDuplicated_ThrowsException()
        {
            IBoundProperty<object> boundProperty1 = Substitute.For<IBoundProperty<object>>();
            IBoundProperty<object> boundProperty2 = Substitute.For<IBoundProperty<object>>();
            const string key1 = nameof(key1);
            const string key2 = nameof(key1);
            boundProperty1.Key.Returns(key1);
            boundProperty2.Key.Returns(key2);

            _boundPropertyContainer.Add(boundProperty1);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _boundPropertyContainer.Add(boundProperty2));
            Assert.AreEqual($"Cannot add bound property with Type: {typeof(object)} and Key: {key2}", invalidOperationException.Message);
        }

        [Test]
        public void Add_KeyNotDuplicated_DoesNotThrowsException()
        {
            IBoundProperty<object> boundProperty1 = Substitute.For<IBoundProperty<object>>();
            IBoundProperty<object> boundProperty2 = Substitute.For<IBoundProperty<object>>();
            const string key1 = nameof(key1);
            const string key2 = nameof(key2);
            boundProperty1.Key.Returns(key1);
            boundProperty2.Key.Returns(key2);

            _boundPropertyContainer.Add(boundProperty1);

            Assert.DoesNotThrow(() => _boundPropertyContainer.Add(boundProperty2));
        }

        [Test]
        public void Get_KeyAdded_ReturnsExpectedValue()
        {
            IBoundProperty<object> expectedboundProperty = Substitute.For<IBoundProperty<object>>();
            const string key = nameof(key);
            expectedboundProperty.Key.Returns(key);
            _boundPropertyContainer.Add(expectedboundProperty);

            IBoundProperty<object> boundProperty = _boundPropertyContainer.Get<object>(key);

            Assert.AreSame(expectedboundProperty, boundProperty);
        }

        [Test]
        public void Get_KeyNotAdded_ThrowsException()
        {
            const string key = nameof(key);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => { IBoundProperty<object> _ = _boundPropertyContainer.Get<object>(key); });
            Assert.AreEqual($"Cannot get bound property with Type: {typeof(object)} and Key: {key}", invalidOperationException.Message);
        }
    }
}