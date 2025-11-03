using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using Infrastructure.ModelViewViewModel;
using Infrastructure.ModelViewViewModel.Composition;
using Infrastructure.ModelViewViewModel.PropertyBindings;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.ModelViewViewModel.Composition
{
    public class ModelViewViewModelComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private ScopeBuildingContext _scopeBuildingContext;
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;

        private ModelViewViewModelComposer _modelViewViewModelComposer;

        [SetUp]
        public void SetUp()
        {
            _scopeBuildingContext = new ScopeBuildingContext();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();

            _modelViewViewModelComposer = new ModelViewViewModelComposer();
        }

        [Test]
        public void AddRules_AddExpected()
        {
            IRule<IBoundPropertyContainer> boundPropertyContainerRule = Substitute.For<IRule<IBoundPropertyContainer>>();
            IRule<IBoundMethodContainer> boundMethodContainerRule = Substitute.For<IRule<IBoundMethodContainer>>();
            _ruleFactory.GetTransient(Arg.Any<Func<IRuleResolver, IBoundPropertyContainer>>()).Returns(boundPropertyContainerRule);
            _ruleFactory.GetTransient(Arg.Any<Func<IRuleResolver, IBoundMethodContainer>>()).Returns(boundMethodContainerRule);
            _modelViewViewModelComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(boundPropertyContainerRule);
            _ruleAdder.Received(1).Add(boundMethodContainerRule);
        }

        [Test]
        public void AddSharedRules_AddExpected()
        {
            IRule<Action<CollectionBindingDependenciesContainer>> collectionBindingDependenciesContainerRule = Substitute.For<IRule<Action<CollectionBindingDependenciesContainer>>>();
            IRule<Action<ViewModel>> viewModelRule = Substitute.For<IRule<Action<ViewModel>>>();
            _ruleFactory.GetInject(Arg.Any<Action<IRuleResolver, CollectionBindingDependenciesContainer>>()).Returns(collectionBindingDependenciesContainerRule);
            _ruleFactory.GetInject(Arg.Any<Action<IRuleResolver, ViewModel>>()).Returns(viewModelRule);
            _modelViewViewModelComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddSharedRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(collectionBindingDependenciesContainerRule);
            _ruleAdder.Received(1).Add(viewModelRule);
        }
    }
}