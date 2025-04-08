using System.Collections.Generic;
using System.Linq;
using Infrastructure.Tweening;
using Infrastructure.Tweening.Builders;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening.Builders
{
    public class SequenceBuilderTests
    {
        // It also covers SequenceBaseBuilderHelper tests

        private SequenceBuilder _sequenceBuilder;

        [SetUp]
        public void SetUp()
        {
            _sequenceBuilder = new SequenceBuilder();
        }

        #region SequenceBaseBuilderHelper

        [Test]
        public void Tweens_NotSet_ReturnsNotNullAndEmpty()
        {
            IEnumerable<ITween> result = _sequenceBuilder.Tweens;

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Tweens_AddTween_ReturnsNotNullAndTweenIsAdded()
        {
            ITween tween = Substitute.For<ITween>();
            _sequenceBuilder.AddTween(tween);

            IEnumerable<ITween> result = _sequenceBuilder.Tweens;

            Assert.IsNotNull(result);

            List<ITween> resultList = result.ToList();

            Assert.IsTrue(resultList.Count == 1);
            Assert.IsTrue(resultList.Contains(tween));
        }

        #endregion
    }
}