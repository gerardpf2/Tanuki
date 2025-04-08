using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.Builders;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening.Builders
{
    public class SequenceAsyncBuilderTests
    {
        private SequenceAsyncBuilder _sequenceAsyncBuilder;

        [SetUp]
        public void SetUp()
        {
            _sequenceAsyncBuilder = new SequenceAsyncBuilder();
        }

        [Test]
        public void Build_ReturnsExpected()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 1.0f;
            const float delayAfterS = 2.0f;
            const int repetitions = 1;
            const RepetitionType repetitionType = RepetitionType.Yoyo;
            const DelayManagement delayManagementRepetition = DelayManagement.Before;
            const DelayManagement delayManagementRestart = DelayManagement.After;
            Action onStartIteration = Substitute.For<Action>();
            Action onStartPlay = Substitute.For<Action>();
            Action onEndPlay = Substitute.For<Action>();
            Action onEndIteration = Substitute.For<Action>();
            Action onPause = Substitute.For<Action>();
            Action onResume = Substitute.For<Action>();
            Action onRestart = Substitute.For<Action>();
            Action onComplete = Substitute.For<Action>();
            ITween expectedResult =
                new SequenceAsync(
                    autoPlay,
                    delayBeforeS,
                    delayAfterS,
                    repetitions,
                    repetitionType,
                    delayManagementRepetition,
                    delayManagementRestart,
                    onStartIteration,
                    onStartPlay,
                    onEndPlay,
                    onEndIteration,
                    onPause,
                    onResume,
                    onRestart,
                    onComplete,
                    _sequenceAsyncBuilder.Tweens
                );
            _sequenceAsyncBuilder
                .WithAutoPlay(autoPlay)
                .WithDelayBeforeS(delayBeforeS)
                .WithDelayAfterS(delayAfterS)
                .WithRepetitions(repetitions)
                .WithRepetitionType(repetitionType)
                .WithDelayManagementRepetition(delayManagementRepetition)
                .WithDelayManagementRestart(delayManagementRestart)
                .WithOnStartIteration(onStartIteration)
                .WithOnStartPlay(onStartPlay)
                .WithOnEndPlay(onEndPlay)
                .WithOnEndIteration(onEndIteration)
                .WithOnPause(onPause)
                .WithOnResume(onResume)
                .WithOnRestart(onRestart)
                .WithOnComplete(onComplete);

            ITween result = _sequenceAsyncBuilder.Build();

            Assert.AreEqual(expectedResult, result);
        }
    }
}