using System;
using Infrastructure.System;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.System
{
    public class ComparerTests
    {
        private IComparable _valueA;
        private IComparable _valueB;

        private Comparer _comparer;

        [SetUp]
        public void SetUp()
        {
            _valueA = Substitute.For<IComparable>();
            _valueB = null;

            _comparer = new Comparer();
        }

        [TestCase(-1, ComparisonOperator.EqualTo, false)]
        [TestCase(0, ComparisonOperator.EqualTo, true)]
        [TestCase(1, ComparisonOperator.EqualTo, false)]
        [TestCase(-1, ComparisonOperator.UnequalTo, true)]
        [TestCase(0, ComparisonOperator.UnequalTo, false)]
        [TestCase(1, ComparisonOperator.UnequalTo, true)]
        [TestCase(-1, ComparisonOperator.LessThan, true)]
        [TestCase(0, ComparisonOperator.LessThan, false)]
        [TestCase(1, ComparisonOperator.LessThan, false)]
        [TestCase(-1, ComparisonOperator.GreaterThan, false)]
        [TestCase(0, ComparisonOperator.GreaterThan, false)]
        [TestCase(1, ComparisonOperator.GreaterThan, true)]
        [TestCase(-1, ComparisonOperator.LessThanOrEqualTo, true)]
        [TestCase(0, ComparisonOperator.LessThanOrEqualTo, true)]
        [TestCase(1, ComparisonOperator.LessThanOrEqualTo, false)]
        [TestCase(-1, ComparisonOperator.GreaterThanOrEqualTo, false)]
        [TestCase(0, ComparisonOperator.GreaterThanOrEqualTo, true)]
        [TestCase(1, ComparisonOperator.GreaterThanOrEqualTo, true)]
        public void IsTrueThat_ReturnsExpected(int compareToResult, ComparisonOperator comparisonOperator, bool expectedResult)
        {
            _valueA.CompareTo(_valueB).Returns(compareToResult);

            bool result = _comparer.IsTrueThat(_valueA, comparisonOperator, _valueB);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void IsTrueThat_ComparisonOperatorOutOfRange_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { bool _ = _comparer.IsTrueThat(_valueA, ComparisonOperator.EqualTo - 1, _valueB); });
        }
    }
}