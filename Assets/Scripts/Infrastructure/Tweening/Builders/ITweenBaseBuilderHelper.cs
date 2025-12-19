using System;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.Builders
{
    public interface ITweenBaseBuilderHelper<out TBuilder, out TTween>
    {
        [NotNull]
        TBuilder WithAutoPlay(bool autoPlay);

        [NotNull]
        TBuilder WithDelayBeforeS(float delayBeforeS);

        [NotNull]
        TBuilder WithDelayAfterS(float delayAfterS);

        [NotNull]
        TBuilder WithRepetitions(int repetitions);

        [NotNull]
        TBuilder WithRepetitionType(RepetitionType repetitionType);

        [NotNull]
        TBuilder WithDelayManagementRepetition(DelayManagement delayManagementRepetition);

        [NotNull]
        TBuilder WithDelayManagementRestart(DelayManagement delayManagementRestart);

        [NotNull]
        TBuilder WithOnStep(Action<TTween> onStep);

        [NotNull]
        TBuilder WithOnStartIteration(Action<TTween> onStartIteration);

        [NotNull]
        TBuilder WithOnStartPlay(Action<TTween> onStartPlay);

        [NotNull]
        TBuilder WithOnPlay(Action<TTween> onPlay);

        [NotNull]
        TBuilder WithOnEndPlay(Action<TTween> onEndPlay);

        [NotNull]
        TBuilder WithOnEndIteration(Action<TTween> onEndIteration);

        [NotNull]
        TBuilder WithOnComplete(Action<TTween> onComplete);

        [NotNull]
        TBuilder WithOnPause(Action<TTween> onPause);

        [NotNull]
        TBuilder WithOnResume(Action<TTween> onResume);

        [NotNull]
        TBuilder WithOnRestart(Action<TTween> onRestart);

        [NotNull]
        ITweenBase Build();
    }
}