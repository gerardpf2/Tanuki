using System;
using Infrastructure.Gating;
using Infrastructure.System;
using Infrastructure.Unity;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Gating
{
    public class GateValidatorTests
    {
        private IGateDefinitionGetter _gateDefinitionGetter;
        private IProjectVersionGetter _projectVersionGetter;
        private Func<string, bool> _configValueGetter;
        private IComparer _comparer;
        private string _projectVersion;

        private GateValidator _gateValidator;

        [SetUp]
        public void SetUp()
        {
            _gateDefinitionGetter = Substitute.For<IGateDefinitionGetter>();
            _projectVersionGetter = Substitute.For<IProjectVersionGetter>();
            _configValueGetter = Substitute.For<Func<string, bool>>();
            _comparer = Substitute.For<IComparer>();
            _projectVersion = "1.0";

            _projectVersionGetter.Get().Returns(_projectVersion);

            // Needs to be done after _projectVersionGetter setup
            _gateValidator = new GateValidator(_gateDefinitionGetter, _configValueGetter, _projectVersionGetter, _comparer);
        }

        [Test]
        public void Validate_GateKeyNull_ReturnsTrue()
        {
            bool result = _gateValidator.Validate(null);

            Assert.IsTrue(result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Validate_UseConfig_ReturnsExpected(bool expectedResult)
        {
            const string configKey = nameof(configKey);
            IGateDefinition gateDefinition = Substitute.For<IGateDefinition>();
            gateDefinition.UseConfig.Returns(true);
            gateDefinition.ConfigKey.Returns(configKey);
            gateDefinition.UseVersion.Returns(false);
            const string gateKey = nameof(gateKey);
            _gateDefinitionGetter.Get(gateKey).Returns(gateDefinition);
            _configValueGetter.Invoke(configKey).Returns(expectedResult);

            bool result = _gateValidator.Validate(gateKey);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Validate_UseVersion_ReturnsExpected(bool expectedResult)
        {
            const string gateVersion = "1.1";
            IGateDefinition gateDefinition = Substitute.For<IGateDefinition>();
            gateDefinition.UseConfig.Returns(false);
            gateDefinition.UseVersion.Returns(true);
            gateDefinition.Version.Returns(gateVersion);
            const string gateKey = nameof(gateKey);
            _gateDefinitionGetter.Get(gateKey).Returns(gateDefinition);
            _comparer
                .IsTrueThat(
                    Arg.Is<Version>(version => version.ToString() == _projectVersion),
                    Arg.Any<ComparisonOperator>(),
                    Arg.Is<Version>(version => version.ToString() == gateVersion)
                )
                .Returns(expectedResult);

            bool result = _gateValidator.Validate(gateKey);

            Assert.AreEqual(expectedResult, result);
        }
    }
}