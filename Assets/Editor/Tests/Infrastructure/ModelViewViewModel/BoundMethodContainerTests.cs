using System;
using Infrastructure.ModelViewViewModel;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.ModelViewViewModel
{
    public class BoundMethodContainerTests
    {
        private BoundMethodContainer _boundMethodContainer;

        [SetUp]
        public void SetUp()
        {
            _boundMethodContainer = new BoundMethodContainer();
        }

        [Test]
        public void Add_KeyNull_ThrowsException()
        {
            IBoundMethod boundMethod = Substitute.For<IBoundMethod>();
            const string key = null;
            boundMethod.Key.Returns(key);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _boundMethodContainer.Add(boundMethod));
            Assert.AreEqual($"Cannot add bound method with Key: {key}", invalidOperationException.Message);
        }

        [Test]
        public void Add_KeyDuplicated_ThrowsException()
        {
            IBoundMethod boundMethod1 = Substitute.For<IBoundMethod>();
            IBoundMethod boundMethod2 = Substitute.For<IBoundMethod>();
            const string key1 = nameof(key1);
            const string key2 = nameof(key1);
            boundMethod1.Key.Returns(key1);
            boundMethod2.Key.Returns(key2);

            _boundMethodContainer.Add(boundMethod1);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _boundMethodContainer.Add(boundMethod2));
            Assert.AreEqual($"Cannot add bound method with Key: {key2}", invalidOperationException.Message);
        }

        [Test]
        public void Add_KeyNotDuplicated_DoesNotThrowsException()
        {
            IBoundMethod boundMethod1 = Substitute.For<IBoundMethod>();
            IBoundMethod boundMethod2 = Substitute.For<IBoundMethod>();
            const string key1 = nameof(key1);
            const string key2 = nameof(key2);
            boundMethod1.Key.Returns(key1);
            boundMethod2.Key.Returns(key2);

            _boundMethodContainer.Add(boundMethod1);

            Assert.DoesNotThrow(() => _boundMethodContainer.Add(boundMethod2));
        }

        [Test]
        public void Get_KeyAdded_ReturnsExpectedValue()
        {
            IBoundMethod expectedBoundMethod = Substitute.For<IBoundMethod>();
            const string key = nameof(key);
            expectedBoundMethod.Key.Returns(key);
            _boundMethodContainer.Add(expectedBoundMethod);

            IBoundMethod boundMethod = _boundMethodContainer.Get(key);

            Assert.AreSame(expectedBoundMethod, boundMethod);
        }

        [Test]
        public void Get_KeyNotAdded_ThrowsException()
        {
            const string key = nameof(key);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => { IBoundMethod _ = _boundMethodContainer.Get(key); });
            Assert.AreEqual($"Cannot get bound method with Key: {key}", invalidOperationException.Message);
        }
    }
}