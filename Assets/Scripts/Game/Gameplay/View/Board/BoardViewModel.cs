using System;
using Game.Gameplay.Board;
using Game.Gameplay.View.Camera;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
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

        private IBoardViewController _boardViewController;
        private ICameraController _cameraController;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] IBoardViewController boardViewController,
            [NotNull] ICameraController cameraController)
        {
            ArgumentNullException.ThrowIfNull(boardViewController);
            ArgumentNullException.ThrowIfNull(cameraController);

            _boardViewController = boardViewController;
            _cameraController = cameraController;
        }

        public void SetData([NotNull] BoardViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            Initialize(data.Board, data.OnViewReady);
        }

        private void Initialize(IReadonlyBoard board, Action onViewReady)
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            InvalidOperationException.ThrowIfNull(_top);
            InvalidOperationException.ThrowIfNull(_bottom);
            InvalidOperationException.ThrowIfNull(_boardViewController);
            InvalidOperationException.ThrowIfNull(_cameraController);

            _boardViewController.Initialize(board);
            _cameraController.Initialize(board, _top.position.y, _bottom.position.y);

            onViewReady?.Invoke();
        }
    }
}