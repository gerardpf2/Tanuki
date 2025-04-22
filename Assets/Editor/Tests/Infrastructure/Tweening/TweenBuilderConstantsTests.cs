using Infrastructure.Tweening;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class TweenBuilderConstantsTests
    {
        [Test]
        public void EasingType_ReturnsOutQuad()
        {
            Assert.AreEqual(EasingType.OutQuad, TweenBuilderConstants.EasingType);
        }
    }
}