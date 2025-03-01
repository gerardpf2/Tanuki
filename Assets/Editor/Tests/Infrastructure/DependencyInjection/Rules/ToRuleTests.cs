using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection.Rules
{
    public class ToRuleTests
    {
        private IRuleResolver _ruleResolver;
        private object _toResolveResult;
        private object _keyToResolve;

        private ToRule<object, object> _toRule;

        [SetUp]
        public void SetUp()
        {
            _ruleResolver = Substitute.For<IRuleResolver>();
            _toResolveResult = new object();
            _keyToResolve = new object();

            _toRule = new ToRule<object, object>(_keyToResolve);

            _ruleResolver.Resolve<object>(_keyToResolve).Returns(_toResolveResult);
        }

        [Test]
        public void Resolve_ReturnsToResolveResult()
        {
            object result = _toRule.Resolve(_ruleResolver);

            Assert.AreSame(_toResolveResult, result);
        }
    }
}