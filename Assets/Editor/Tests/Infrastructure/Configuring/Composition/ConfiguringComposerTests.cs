using Infrastructure.Configuring;
using Infrastructure.Configuring.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Configuring.Composition
{
    public class ConfiguringComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private IRule<IConfigValueGetter> _configValueGetterRule;
        private ScopeBuildingContext _scopeBuildingContext;
        private IConfigValueGetter _configValueGetter;
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;

        private ConfiguringComposer _configuringComposer;

        [SetUp]
        public void SetUp()
        {
            _configValueGetterRule = Substitute.For<IRule<IConfigValueGetter>>();
            _configValueGetter = Substitute.For<IConfigValueGetter>();
            _scopeBuildingContext = new ScopeBuildingContext();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();

            _configuringComposer = new ConfiguringComposer(_configValueGetter);

            _ruleFactory.GetInstance(_configValueGetter).Returns(_configValueGetterRule);
            _configuringComposer.Compose(_scopeBuildingContext);
        }

        [Test]
        public void AddRules_AddExpected()
        {
            _scopeBuildingContext.AddRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(_configValueGetterRule);
        }
    }
}