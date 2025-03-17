using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Utils;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection.Utils
{
    public class ScopeBuilderUtilsTests
    {
        private IScopeComposer _scopeComposer;
        private IScopeBuilder _scopeBuilder;

        [SetUp]
        public void SetUp()
        {
            _scopeComposer = Substitute.For<IScopeComposer>();
            _scopeBuilder = Substitute.For<IScopeBuilder>();
        }

        [Test]
        public void BuildRoot_BuildCalledWithValidParams()
        {
            _scopeBuilder.BuildRoot(_scopeComposer);

            _scopeBuilder.Received(1).Build(null, _scopeComposer);
        }
    }
}