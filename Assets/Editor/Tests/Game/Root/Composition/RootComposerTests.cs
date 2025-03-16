using System.Collections.Generic;
using System.Linq;
using Game.Root.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel.Composition;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Game.Root.Composition
{
    public class RootComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private ScopeBuildingContext _scopeBuildingContext;
        private IRuleResolver _ruleResolver;

        private RootComposer _rootComposer;

        [SetUp]
        public void SetUp()
        {
            _scopeBuildingContext = new ScopeBuildingContext();
            _ruleResolver = Substitute.For<IRuleResolver>();

            _rootComposer = new RootComposer();
        }

        [Test]
        public void GetChildScopeComposers_ReturnsExpected()
        {
            _rootComposer.Compose(_scopeBuildingContext);

            List<IScopeComposer> childScopeComposers = _scopeBuildingContext.GetChildScopeComposers(_ruleResolver).ToList();

            Assert.IsTrue(childScopeComposers.Count == 1);
            Assert.NotNull(childScopeComposers.Find(childScopeComposer => childScopeComposer is ModelViewViewModelComposer));
        }
    }
}