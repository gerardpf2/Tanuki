using Infrastructure.Configuring;
using Infrastructure.System;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Configuring
{
    public class ConfigValueGetterTests
    {
        private IConfigDefinitionGetter _configDefinitionGetter;
        private IConfigDefinition _configDefinition;
        private string _configDefinitionValue;
        private object _expectedResult;
        private IConverter _converter;
        private string _configKey;

        private ConfigValueGetter _configValueGetter;

        [SetUp]
        public void SetUp()
        {
            _configDefinitionGetter = Substitute.For<IConfigDefinitionGetter>();
            _configDefinition = Substitute.For<IConfigDefinition>();
            _configDefinitionValue = nameof(_configDefinitionValue);
            _converter = Substitute.For<IConverter>();
            _configKey = nameof(_configKey);
            _expectedResult = new object();

            _configValueGetter = new ConfigValueGetter(_configDefinitionGetter, _converter);

            _configDefinition.Value.Returns(_configDefinitionValue);
            _configDefinitionGetter.Get(_configKey).Returns(_configDefinition);
            _converter.Convert<object>(_configDefinitionValue).Returns(_expectedResult);
        }

        [Test]
        public void Get_ReturnsExpected()
        {
            object result = _configValueGetter.Get<object>(_configKey);

            Assert.AreSame(_expectedResult, result);
        }
    }
}