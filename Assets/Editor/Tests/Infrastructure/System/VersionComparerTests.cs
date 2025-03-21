using System;
using Infrastructure.System;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.System
{
    public class VersionComparerTests
    {
        private VersionComparer _versionComparer;

        [SetUp]
        public void SetUp()
        {
            _versionComparer = new VersionComparer();
        }

        [TestCase("1.0", ComparisonOperator.EqualTo, "1.0", true)]
        [TestCase("0.9", ComparisonOperator.EqualTo, "1.0", false)]
        [TestCase("1.1", ComparisonOperator.EqualTo, "1.0", false)]
        [TestCase("1.0", ComparisonOperator.UnequalTo, "1.0", false)]
        [TestCase("0.9", ComparisonOperator.UnequalTo, "1.0", true)]
        [TestCase("1.1", ComparisonOperator.UnequalTo, "1.0", true)]
        [TestCase("1.0", ComparisonOperator.LessThan, "1.0", false)]
        [TestCase("0.9", ComparisonOperator.LessThan, "1.0", true)]
        [TestCase("1.1", ComparisonOperator.LessThan, "1.0", false)]
        [TestCase("1.0", ComparisonOperator.GreaterThan, "1.0", false)]
        [TestCase("0.9", ComparisonOperator.GreaterThan, "1.0", false)]
        [TestCase("1.1", ComparisonOperator.GreaterThan, "1.0", true)]
        [TestCase("1.0", ComparisonOperator.LessThanOrEqualTo, "1.0", true)]
        [TestCase("0.9", ComparisonOperator.LessThanOrEqualTo, "1.0", true)]
        [TestCase("1.1", ComparisonOperator.LessThanOrEqualTo, "1.0", false)]
        [TestCase("1.0", ComparisonOperator.GreaterThanOrEqualTo, "1.0", true)]
        [TestCase("0.9", ComparisonOperator.GreaterThanOrEqualTo, "1.0", false)]
        [TestCase("1.1", ComparisonOperator.GreaterThanOrEqualTo, "1.0", true)]
        public void IsTrueThat_ReturnsExpected(
            string versionAStr,
            ComparisonOperator comparisonOperator,
            string versionBStr,
            bool expectedResult)
        {
            Version versionA = new(versionAStr);
            Version versionB = new(versionBStr);

            bool result = _versionComparer.IsTrueThat(versionA, comparisonOperator, versionB);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void IsTrueThat_ComparisonOperatorOutOfRange_ThrowsException()
        {
            Version versionA = new("1.0");
            Version versionB = new("1.0");
            const ComparisonOperator comparisonOperator = ComparisonOperator.EqualTo - 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => { bool _ = _versionComparer.IsTrueThat(versionA, comparisonOperator, versionB); });
        }
    }
}