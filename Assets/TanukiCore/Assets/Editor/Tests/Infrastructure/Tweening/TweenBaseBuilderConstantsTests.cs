using Infrastructure.Tweening;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class TweenBaseBuilderConstantsTests
    {
        [Test]
        public void AutoPlay_ReturnsTrue()
        {
            Assert.IsTrue(TweenBaseBuilderConstants.AutoPlay);
        }

        [Test]
        public void RepetitionType_ReturnsRestart()
        {
            Assert.AreEqual(RepetitionType.Restart, TweenBaseBuilderConstants.RepetitionType);
        }

        [Test]
        public void DelayManagementRepetition_ReturnsBeforeAndAfter()
        {
            Assert.AreEqual(DelayManagement.BeforeAndAfter, TweenBaseBuilderConstants.DelayManagementRepetition);
        }

        [Test]
        public void DelayManagementRestart_ReturnsBeforeAndAfter()
        {
            Assert.AreEqual(DelayManagement.BeforeAndAfter, TweenBaseBuilderConstants.DelayManagementRestart);
        }
    }
}