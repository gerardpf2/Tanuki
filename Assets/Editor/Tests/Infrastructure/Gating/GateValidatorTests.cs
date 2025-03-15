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

        private GateValidator _gateValidator;

        [SetUp]
        public void SetUp()
        {
            _gateDefinitionGetter = Substitute.For<IGateDefinitionGetter>();
            _projectVersionGetter = Substitute.For<IProjectVersionGetter>();
        }

        [Test]
        public void Validate_GateKeyNull_ReturnsTrue()
        {
            const string projectVersion = "1.0";
            _projectVersionGetter.Get().Returns(projectVersion);
            _gateValidator = new GateValidator(_gateDefinitionGetter, _projectVersionGetter);

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
            string projectVersion,
            string gateVersion,
            ComparisonOperator comparisonOperator,
            bool expectedResult)
        {
            _projectVersionGetter.Get().Returns(projectVersion);
            _gateValidator = new GateValidator(_gateDefinitionGetter, _projectVersionGetter);
            IGateDefinition gateDefinition = Substitute.For<IGateDefinition>();
            gateDefinition.UseVersion.Returns(true);
            gateDefinition.Version.Returns(gateVersion);
            gateDefinition.VersionComparisonOperator.Returns(comparisonOperator);
            const string gateKey = nameof(gateKey);
            _gateDefinitionGetter.Get(gateKey).Returns(gateDefinition);

            bool result = _gateValidator.Validate(gateKey);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Validate_UseVersionAndComparisonOperatorOutOfRange_ThrowsException()
        {
            const string projectVersion = "1.0";
            _projectVersionGetter.Get().Returns(projectVersion);
            _gateValidator = new GateValidator(_gateDefinitionGetter, _projectVersionGetter);
            const string gateVersion = "1.0";
            const ComparisonOperator versionComparisonOperator = ComparisonOperator.EqualTo - 1;
            IGateDefinition gateDefinition = Substitute.For<IGateDefinition>();
            gateDefinition.UseVersion.Returns(true);
            gateDefinition.Version.Returns(gateVersion);
            gateDefinition.VersionComparisonOperator.Returns(versionComparisonOperator);
            const string gateKey = nameof(gateKey);
            _gateDefinitionGetter.Get(gateKey).Returns(gateDefinition);

            Assert.Throws<ArgumentOutOfRangeException>(() => { bool _ = _gateValidator.Validate(gateKey); });
        }
    }
}