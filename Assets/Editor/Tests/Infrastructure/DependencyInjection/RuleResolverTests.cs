using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class RuleResolverTests
    {
        private IRuleResolver _parentRuleResolver;
        private IRuleGetter _publicRuleGetter;
        private IRule<object> _rule;
        private object _key;

        private IRuleResolver _ruleResolver;

        [SetUp]
        public void SetUp()
        {
            _parentRuleResolver = Substitute.For<IRuleResolver>();
            _publicRuleGetter = Substitute.For<IRuleGetter>();
            _rule = Substitute.For<IRule<object>>();
            _key = new object();
        }

        [Test]
        public void Resolve_FoundAndResultNotNull_ReturnsResult()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, null);

            // Found
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(r => { r[0] = _rule; return true; });
            // ResultNotNull
            object expectedResult = new();
            _rule.Resolve(_ruleResolver).Returns(expectedResult);

            object result = _ruleResolver.Resolve<object>(_key);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void Resolve_FoundAndResultNullAndParentReturnsTrue_ReturnsParentResult()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, _parentRuleResolver);

            // Found
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(r => { r[0] = _rule; return true; });
            // ResultNull
            _rule.Resolve(_ruleResolver).Returns(null);
            // ParentReturnsTrue
            object expectedResult = new();
            _parentRuleResolver.TryResolve(_ruleResolver, out Arg.Any<IRule<object>>(), _key).Returns(r => { r[1] = expectedResult; return true; });

            object result = _ruleResolver.Resolve<object>(_key);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void Resolve_FoundAndResultNullAndParentReturnsFalse_ThrowsException()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, _parentRuleResolver);

            // Found
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(r => { r[0] = _rule; return true; });
            // ResultNull
            _rule.Resolve(_ruleResolver).Returns(null);
            // ParentReturnsFalse
            _parentRuleResolver.TryResolve(out Arg.Any<IRule<object>>(), _key).Returns(false);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _ruleResolver.Resolve<object>(_key));
            Assert.AreEqual($"Cannot resolve rule with Type: {typeof(object)} and Key: {_key}", invalidOperationException.Message);
        }

        [Test]
        public void Resolve_FoundAndResultNullAndNoParent_ThrowsException()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, null);

            // Found
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(r => { r[0] = _rule; return true; });
            // ResultNull
            _rule.Resolve(_ruleResolver).Returns(null);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _ruleResolver.Resolve<object>(_key));
            Assert.AreEqual($"Cannot resolve rule with Type: {typeof(object)} and Key: {_key}", invalidOperationException.Message);
        }

        [Test]
        public void Resolve_NotFoundAndParentReturnsTrue_ReturnsParentResult()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, _parentRuleResolver);

            // NotFound
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(false);
            // ParentReturnsTrue
            object expectedResult = new();
            _parentRuleResolver.TryResolve(_ruleResolver, out Arg.Any<IRule<object>>(), _key).Returns(r => { r[1] = expectedResult; return true; });

            object result = _ruleResolver.Resolve<object>(_key);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void Resolve_NotFoundAndParentReturnsFalse_ThrowsException()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, _parentRuleResolver);

            // NotFound
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(false);
            // ParentReturnsFalse
            _parentRuleResolver.TryResolve(out Arg.Any<IRule<object>>(), _key).Returns(false);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _ruleResolver.Resolve<object>(_key));
            Assert.AreEqual($"Cannot resolve rule with Type: {typeof(object)} and Key: {_key}", invalidOperationException.Message);
        }

        [Test]
        public void Resolve_NotFoundAndNoParent_ThrowsException()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, null);

            // NotFound
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(false);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _ruleResolver.Resolve<object>(_key));
            Assert.AreEqual($"Cannot resolve rule with Type: {typeof(object)} and Key: {_key}", invalidOperationException.Message);
        }

        [Test]
        public void TryResolve_FoundAndResultNotNull_ReturnsTrueAndOutResult()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, null);

            // Found
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(r => { r[0] = _rule; return true; });
            // ResultNotNull
            object expectedResult = new();
            _rule.Resolve(_ruleResolver).Returns(expectedResult);

            bool found = _ruleResolver.TryResolve(out object result, _key);

            Assert.IsTrue(found);
            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void TryResolve_FoundAndResultNullAndParentReturnsTrue_ReturnsTrueAndOutParentResult()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, _parentRuleResolver);

            // Found
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(r => { r[0] = _rule; return true; });
            // ResultNull
            _rule.Resolve(_ruleResolver).Returns(null);
            // ParentReturnsTrue
            object expectedResult = new();
            _parentRuleResolver.TryResolve(_ruleResolver, out Arg.Any<IRule<object>>(), _key).Returns(r => { r[1] = expectedResult; return true; });

            bool found = _ruleResolver.TryResolve(out object result, _key);

            Assert.IsTrue(found);
            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void TryResolve_FoundAndResultNullAndParentReturnsFalse_ReturnsFalseAndOutParentResult()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, _parentRuleResolver);

            // Found
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(r => { r[0] = _rule; return true; });
            // ResultNull
            _rule.Resolve(_ruleResolver).Returns(null);
            // ParentReturnsFalse
            object expectedResult = new();
            _parentRuleResolver.TryResolve(_ruleResolver, out Arg.Any<IRule<object>>(), _key).Returns(r => { r[1] = expectedResult; return false; });

            bool found = _ruleResolver.TryResolve(out object result, _key);

            Assert.IsFalse(found);
            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void TryResolve_FoundAndResultNullAndNoParent_ReturnsFalseAndOutNull()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, null);

            // Found
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(r => { r[0] = _rule; return true; });
            // ResultNull
            _rule.Resolve(_ruleResolver).Returns(null);

            bool found = _ruleResolver.TryResolve(out object result, _key);

            Assert.IsFalse(found);
            Assert.IsNull(result);
        }

        [Test]
        public void TryResolve_NotFoundAndParentReturnsTrue_ReturnsTrueAndOutParentResult()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, _parentRuleResolver);

            // NotFound
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(false);
            // ParentReturnsTrue
            object expectedResult = new();
            _parentRuleResolver.TryResolve(_ruleResolver, out Arg.Any<IRule<object>>(), _key).Returns(r => { r[1] = expectedResult; return true; });

            bool found = _ruleResolver.TryResolve(out object result, _key);

            Assert.IsTrue(found);
            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void TryResolve_NotFoundAndParentReturnsFalse_ReturnsFalseAndOutParentResult()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, _parentRuleResolver);

            // NotFound
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(false);
            // ParentReturnsFalse
            object expectedResult = new();
            _parentRuleResolver.TryResolve(_ruleResolver, out Arg.Any<IRule<object>>(), _key).Returns(r => { r[1] = expectedResult; return false; });

            bool found = _ruleResolver.TryResolve(out object result, _key);

            Assert.IsFalse(found);
            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void TryResolve_NotFoundAndNoParent_ReturnsFalseAndOutNull()
        {
            _ruleResolver = new RuleResolver(null, _publicRuleGetter, null);

            // NotFound
            _publicRuleGetter.TryGet(out Arg.Any<IRule<object>>(), _key).Returns(false);

            bool found = _ruleResolver.TryResolve(out object result, _key);

            Assert.IsFalse(found);
            Assert.IsNull(result);
        }
    }
}