using Game.Gameplay.View.Camera;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardViewModel : ViewModel, IDataSettable<BoardViewData>
    {
        [SerializeField] private Transform _top;
        [SerializeField] private Transform _bottom;

        private ICameraView _cameraView;
        private ICoroutineRunnerHelper _coroutineRunnerHelper;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] ICameraView cameraView, [NotNull] ICoroutineRunnerHelper coroutineRunnerHelper)
        {
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(coroutineRunnerHelper);

            _cameraView = cameraView;
            _coroutineRunnerHelper = coroutineRunnerHelper;
        }

        public void SetData([NotNull] BoardViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);
            InvalidOperationException.ThrowIfNull(_coroutineRunnerHelper);

            // Wait for end of frame so UI stuff has been properly updated
            // For example, CameraView will move camera to (0, 0) but UI refreshes later

            _coroutineRunnerHelper.RunWaitForEndOfFrame(
                () =>
                {
                    Initialize();

                    data.OnReady?.Invoke();
                }
            );
        }

        private void Initialize()
        {
            InvalidOperationException.ThrowIfNull(_top);
            InvalidOperationException.ThrowIfNull(_bottom);
            InvalidOperationException.ThrowIfNull(_cameraView);

            _cameraView.SetBoardViewLimits(_top.position.y, _bottom.position.y);
        }
    }
}