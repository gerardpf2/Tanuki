using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Utils;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection.Utils
{
    public class RuleResolverUtilsTests
    {
        private IRuleResolver _ruleResolver;
        private Action<object> _action;
        private object _key;

        [SetUp]
        public void SetUp()
        {
            _ruleResolver = Substitute.For<IRuleResolver>();
            _action = Substitute.For<Action<object>>();
            _key = new object();
        }

        [Test]
        public void SafeResolve_TryResolveCalledWithValidParams()
        {
            _ruleResolver.SafeResolve<object>(_key);

            _ruleResolver.Received(1).TryResolve(out Arg.Any<object>(), _key);
        }

        [Test]
        public void SafeExecute_TryResolveReturnsTrue_ActionCalledWithValidParams()
        {
            object result = new();
            _ruleResolver.TryResolve(out Arg.Any<object>(), _key).Returns(r => { r[0] = result; return true; });

            _ruleResolver.SafeExecute(_action, _key);

            _action.Received(1).Invoke(result);
        }

        [Test]
        public void SafeExecute_TryResolveReturnsFalse_ActionNotCalled()
        {
            _ruleResolver.TryResolve(out Arg.Any<object>(), _key).Returns(false);

            _ruleResolver.SafeExecute(_action, _key);

            _action.DidNotReceive().Invoke(Arg.Any<object>());
        }
    }
}