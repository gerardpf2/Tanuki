using System;
using Infrastructure.Gating;
using Infrastructure.System;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Gating
{
    public class GateValidatorTests
    {
        private IGateDefinitionGetter _gateDefinitionGetter;
        private IProjectVersionGetter _projectVersionGetter;
        private IVersionParser _versionParser;

        private GateValidator _gateValidator;

        [SetUp]
        public void SetUp()
        {
            _gateDefinitionGetter = Substitute.For<IGateDefinitionGetter>();
            _projectVersionGetter = Substitute.For<IProjectVersionGetter>();
            _versionParser = Substitute.For<IVersionParser>();
        }

        [Test]
        public void Validate_GateKeyNull_ReturnsTrue()
        {
            _gateValidator = new GateValidator(_gateDefinitionGetter, _projectVersionGetter, _versionParser);

            bool result = _gateValidator.Validate(null);

            Assert.IsTrue(result);
        }

        [TestCase("1.0", "1.0", ComparisonOperator.EqualTo, true)]
        [TestCase("0.9", "1.0", ComparisonOperator.EqualTo, false)]
        [TestCase("1.1", "1.0", ComparisonOperator.EqualTo, false)]
        [TestCase("1.0", "1.0", ComparisonOperator.UnequalTo, false)]
        [TestCase("0.9", "1.0", ComparisonOperator.UnequalTo, true)]
        [TestCase("1.1", "1.0", ComparisonOperator.UnequalTo, true)]
        [TestCase("1.0", "1.0", ComparisonOperator.LessThan, false)]
        [TestCase("0.9", "1.0", ComparisonOperator.LessThan, true)]
        [TestCase("1.1", "1.0", ComparisonOperator.LessThan, false)]
        [TestCase("1.0", "1.0", ComparisonOperator.GreaterThan, false)]
        [TestCase("0.9", "1.0", ComparisonOperator.GreaterThan, false)]
        [TestCase("1.1", "1.0", ComparisonOperator.GreaterThan, true)]
        [TestCase("1.0", "1.0", ComparisonOperator.LessThanOrEqualTo, true)]
        [TestCase("0.9", "1.0", ComparisonOperator.LessThanOrEqualTo, true)]
        [TestCase("1.1", "1.0", ComparisonOperator.LessThanOrEqualTo, false)]
        [TestCase("1.0", "1.0", ComparisonOperator.GreaterThanOrEqualTo, true)]
        [TestCase("0.9", "1.0", ComparisonOperator.GreaterThanOrEqualTo, false)]
        [TestCase("1.1", "1.0", ComparisonOperator.GreaterThanOrEqualTo, true)]
        public void Validate_UseVersion_ReturnsExpected(
            string projectVersionStr,
            string gateVersionStr,
            ComparisonOperator comparisonOperator,
            bool expectedResult)
        {
            _projectVersionGetter.Get().Returns(projectVersionStr);
            Version projectVersion = Version.Parse(projectVersionStr);
            _versionParser.Parse(projectVersionStr).Returns(projectVersion);
            // Since project version is cached inside ctor, ctor needs to be called after project version setup
            _gateValidator = new GateValidator(_gateDefinitionGetter, _projectVersionGetter, _versionParser);
            IGateDefinition gateDefinition = Substitute.For<IGateDefinition>();
            gateDefinition.UseVersion.Returns(true);
            gateDefinition.Version.Returns(gateVersionStr);
            gateDefinition.VersionComparisonOperator.Returns(comparisonOperator);
            const string gateKey = nameof(gateKey);
            _gateDefinitionGetter.Get(gateKey).Returns(gateDefinition);
            Version gateVersion = Version.Parse(gateVersionStr);
            _versionParser.Parse(gateVersionStr).Returns(gateVersion);

            bool result = _gateValidator.Validate(gateKey);

            Assert.AreEqual(expectedResult, result);
        }
    }
}