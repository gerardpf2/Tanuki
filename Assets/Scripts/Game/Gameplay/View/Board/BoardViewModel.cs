using System;
using System.Collections;
using Game.Gameplay.View.Camera;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Board
{
    public class BoardViewModel : ViewModel, IDataSettable<BoardViewData>
    {
        [SerializeField] private Transform _top;
        [SerializeField] private Transform _bottom;

        private ICameraBoardViewPropertiesSetter _cameraBoardViewPropertiesSetter;
        private ICoroutineRunner _coroutineRunner;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] ICameraBoardViewPropertiesSetter cameraBoardViewPropertiesSetter,
            [NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(cameraBoardViewPropertiesSetter);
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _cameraBoardViewPropertiesSetter = cameraBoardViewPropertiesSetter;
            _coroutineRunner = coroutineRunner;
        }

        public void SetData([NotNull] BoardViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            // Wait for end of frame so UI stuff has been properly updated
            // For example, CameraController will move camera to (0, 0) but UI refreshes later

            _coroutineRunner.Run(WaitEndFrameAndInitialize(data.OnViewReady));
        }

        private IEnumerator WaitEndFrameAndInitialize(Action onViewReady)
        {
            // TODO: Add CoroutineHelper

            yield return new WaitForEndOfFrame();

            Initialize(onViewReady);
        }

        private void Initialize(Action onViewReady)
        {
            InvalidOperationException.ThrowIfNull(_top);
            InvalidOperationException.ThrowIfNull(_bottom);
            InvalidOperationException.ThrowIfNull(_cameraBoardViewPropertiesSetter);

            _cameraBoardViewPropertiesSetter.SetBoardViewLimits(_top.position.y, _bottom.position.y);

            onViewReady?.Invoke();
        }
    }
}