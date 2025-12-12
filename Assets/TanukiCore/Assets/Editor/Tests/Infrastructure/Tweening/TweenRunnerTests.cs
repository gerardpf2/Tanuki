using System;
using System.Collections;
using Infrastructure.Tweening;
using Infrastructure.Unity;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class TweenRunnerTests
    {
        private ICoroutineRunner _coroutineRunner;
        private IDeltaTimeGetter _deltaTimeGetter;
        private ITween _tween;

        private TweenRunner _tweenRunner;

        [SetUp]
        public void SetUp()
        {
            _coroutineRunner = Substitute.For<ICoroutineRunner>();
            _deltaTimeGetter = Substitute.For<IDeltaTimeGetter>();
            _tween = Substitute.For<ITween>();

            _tweenRunner = new TweenRunner(_coroutineRunner, _deltaTimeGetter);
        }

        [Test]
        public void Run_CoroutineNotRunning_RunCoroutineCalled()
        {
            _tweenRunner.Run(_tween);

            _coroutineRunner.Received(1).Run(Arg.Any<IEnumerator>());
        }

        [Test]
        public void Run_CoroutineRunning_RunCoroutineNotCalled()
        {
            ITween otherTween = Substitute.For<ITween>();
            _tweenRunner.Run(otherTween);

            _coroutineRunner.Received(1).Run(Arg.Any<IEnumerator>());

            _tweenRunner.Run(_tween);

            _coroutineRunner.Received(1).Run(Arg.Any<IEnumerator>());
        }

        [Test]
        public void Run_TweenUpdateCalledWithValidParams()
        {
            _coroutineRunner.Run(Arg.Do<IEnumerator>(update => update.MoveNext()));
            const float deltaTimeS = 1.0f;
            _deltaTimeGetter.Get().Returns(deltaTimeS);

            _tweenRunner.Run(_tween);

            _tween.Received(1).Update(deltaTimeS);
        }

        [Test]
        public void Run_TweenNotComplete_OnTweenRemoveAndStopCoroutineNotCalled()
        {
            _coroutineRunner.Run(
                Arg.Do<IEnumerator>(
                    update =>
                    {
                        update.MoveNext();
                        update.MoveNext();
                    }
                )
            );
            _tween.State.Returns(TweenState.Play);
            Action onRemove = Substitute.For<Action>();

            _tweenRunner.Run(_tween, onRemove);

            onRemove.DidNotReceive().Invoke();
            _coroutineRunner.DidNotReceive().Stop(Arg.Any<Coroutine>());
        }

        [Test]
        public void Run_TweenCompleteAndKeepAliveAfterCompleteNull_OnTweenRemoveAndStopCoroutineCalled()
        {
            _coroutineRunner.Run(
                Arg.Do<IEnumerator>(
                    update =>
                    {
                        update.MoveNext();
                        update.MoveNext();
                    }
                )
            );
            _tween.State.Returns(TweenState.Complete);
            Action onRemove = Substitute.For<Action>();

            _tweenRunner.Run(_tween, onRemove);

            onRemove.Received(1).Invoke();
            _coroutineRunner.Received(1).Stop(Arg.Any<Coroutine>());
        }

        [Test]
        public void Run_TweenCompleteAndKeepAliveAfterCompleteReturnsFalse_OnTweenRemoveAndStopCoroutineCalled()
        {
            _coroutineRunner.Run(
                Arg.Do<IEnumerator>(
                    update =>
                    {
                        update.MoveNext();
                        update.MoveNext();
                    }
                )
            );
            _tween.State.Returns(TweenState.Complete);
            Action onRemove = Substitute.For<Action>();
            Func<bool> keepAliveAfterComplete = Substitute.For<Func<bool>>();
            keepAliveAfterComplete.Invoke().Returns(false);

            _tweenRunner.Run(_tween, onRemove, keepAliveAfterComplete);

            onRemove.Received(1).Invoke();
            _coroutineRunner.Received(1).Stop(Arg.Any<Coroutine>());
        }

        [Test]
        public void Run_TweenCompleteAndKeepAliveAfterCompleteReturnsTrue_OnTweenRemoveAndStopCoroutineNotCalled()
        {
            _coroutineRunner.Run(
                Arg.Do<IEnumerator>(
                    update =>
                    {
                        update.MoveNext();
                        update.MoveNext();
                    }
                )
            );
            _tween.State.Returns(TweenState.Complete);
            Action onRemove = Substitute.For<Action>();
            Func<bool> keepAliveAfterComplete = Substitute.For<Func<bool>>();
            keepAliveAfterComplete.Invoke().Returns(true);

            _tweenRunner.Run(_tween, onRemove, keepAliveAfterComplete);

            onRemove.DidNotReceive().Invoke();
            _coroutineRunner.DidNotReceive().Stop(Arg.Any<Coroutine>());
        }
    }
}