using System;
using Infrastructure.ModelViewViewModel;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.ModelViewViewModel
{
    public class BoundPropertyTests
    {
        [Test]
        public void Key_ReturnsExpectedValue()
        {
            string expectedKey = string.Empty;
            BoundProperty<object> boundProperty = new(expectedKey, null);

            string key = boundProperty.Key;

            Assert.AreSame(expectedKey, key);
        }

        [Test]
        public void GetValue_BoundProperty_ReturnsExpectedValue()
        {
            object expectedValue = new();
            BoundProperty<object> boundProperty = new(null, expectedValue);

            object value = boundProperty.Value;

            Assert.AreSame(expectedValue, value);
        }

        [Test]
        public void GetValue_ReturnsExpectedValue()
        {
            object expectedValue = new();
            BoundProperty<object> boundProperty = new(null, null) { Value = expectedValue };

            object value = boundProperty.Value;

            Assert.AreSame(expectedValue, value);
        }

        [Test]
        public void SetValue_Different_AllListenersCalledWithValidParams()
        {
            object value = new();
            BoundProperty<object> boundProperty = new(null, null);
            Action<object> listener1 = Substitute.For<Action<object>>();
            Action<object> listener2 = Substitute.For<Action<object>>();
            boundProperty.Add(listener1);
            boundProperty.Add(listener2);

            boundProperty.Value = value;

            Received.InOrder(
                () =>
                {
                    listener1.Invoke(null); // Add
                    listener2.Invoke(null); // Add
                    listener1.Invoke(value);
                    listener2.Invoke(value);
                }
            );
        }

        [Test]
        public void SetValue_Same_AllListenersCalledWithValidParams()
        {
            object value = new();
            BoundProperty<object> boundProperty = new(null, value);
            Action<object> listener1 = Substitute.For<Action<object>>();
            Action<object> listener2 = Substitute.For<Action<object>>();
            boundProperty.Add(listener1);
            boundProperty.Add(listener2);

            boundProperty.Value = value;

            Received.InOrder(
                () =>
                {
                    listener1.Invoke(value); // Add
                    listener2.Invoke(value); // Add
                    listener1.Invoke(value);
                    listener2.Invoke(value);
                }
            );
        }

        [Test]
        public void Add_ListenerCalledWithValidParams()
        {
            object value = new();
            BoundProperty<object> boundProperty = new(null, value);
            Action<object> listener = Substitute.For<Action<object>>();

            boundProperty.Add(listener);

            listener.Received(1).Invoke(value);
        }

        [Test]
        public void Remove_ListenerNotCalled()
        {
            object value = new();
            BoundProperty<object> boundProperty = new(null, null);
            Action<object> listener = Substitute.For<Action<object>>();
            boundProperty.Add(listener);

            boundProperty.Remove(listener);
            boundProperty.Value = value;

            listener.DidNotReceive().Invoke(value);
        }
    }
}