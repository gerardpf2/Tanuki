using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using Infrastructure.Tweening;
using Infrastructure.Tweening.BuilderHelpers;
using Infrastructure.Tweening.Composition;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening.Composition
{
    public class TweeningComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private ScopeBuildingContext _scopeBuildingContext;
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;

        private TweeningComposer _tweeningComposer;

        [SetUp]
        public void SetUp()
        {
            _scopeBuildingContext = new ScopeBuildingContext();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();

            _tweeningComposer = new TweeningComposer();
        }

        [Test]
        public void AddRules_AddExpected()
        {
            IRule<ITransformTweenBuilderHelper> transformTweenBuilderHelperRule = Substitute.For<IRule<ITransformTweenBuilderHelper>>();
            IRule<IEasingFunctionFactory> easingFunctionFactoryRule = Substitute.For<IRule<IEasingFunctionFactory>>();
            IRule<IEasingFunctionGetter> easingFunctionGetterRule = Substitute.For<IRule<IEasingFunctionGetter>>();
            IRule<ITweenBuilderFactory> tweenBuilderFactoryRule = Substitute.For<IRule<ITweenBuilderFactory>>();
            IRule<ITweenRunner> tweenRunnerRule = Substitute.For<IRule<ITweenRunner>>();
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, ITransformTweenBuilderHelper>>()).Returns(transformTweenBuilderHelperRule);
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, IEasingFunctionFactory>>()).Returns(easingFunctionFactoryRule);
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, IEasingFunctionGetter>>()).Returns(easingFunctionGetterRule);
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, ITweenBuilderFactory>>()).Returns(tweenBuilderFactoryRule);
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, ITweenRunner>>()).Returns(tweenRunnerRule);
            _tweeningComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(transformTweenBuilderHelperRule);
            _ruleAdder.Received(1).Add(easingFunctionFactoryRule);
            _ruleAdder.Received(1).Add(easingFunctionGetterRule);
            _ruleAdder.Received(1).Add(tweenBuilderFactoryRule);
            _ruleAdder.Received(1).Add(tweenRunnerRule);
        }
    }
}