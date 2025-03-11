using System;
using Infrastructure.ModelViewViewModel;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.ModelViewViewModel
{
    public class BoundMethodTests
    {
        [Test]
        public void Key_BoundMethod1Param_ReturnsExpectedValue()
        {
            const string expectedKey = nameof(Method);
            BoundMethod boundMethod = new(Method);

            string key = boundMethod.Key;

            Assert.AreEqual(expectedKey, key);
        }

        [Test]
        public void Call_BoundMethod1Param_MethodInvokeIsCalled()
        {
            Action method = Substitute.For<Action>();
            BoundMethod boundMethod = new(method);

            boundMethod.Call();

            method.Received(1).Invoke();
        }

        [Test]
        public void Key_BoundMethod2Params_ReturnsExpectedValue()
        {
            string expectedKey = string.Empty;
            BoundMethod boundMethod = new(expectedKey, Method);

            string key = boundMethod.Key;

            Assert.AreSame(expectedKey, key);
        }

        [Test]
        public void Call_BoundMethod2Params_MethodInvokeIsCalled()
        {
            Action method = Substitute.For<Action>();
            BoundMethod boundMethod = new(null, method);

            boundMethod.Call();

            method.Received(1).Invoke();
        }

        private void Method() { }
    }
}