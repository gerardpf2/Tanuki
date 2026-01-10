using Game.Common.Pieces;
using Game.Common.View.Pieces;
using Game.Gameplay.View.Bag;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.ModelViewViewModel.Examples.Button;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Player.Input
{
    public class SwapCurrentNextButtonViewModel : ButtonViewModel
    {
        [SerializeField] private PieceSpriteContainer _pieceSpriteContainer;

        private IBagView _bagView;

        [NotNull] private readonly IBoundProperty<Sprite> _pieceSprite = new BoundProperty<Sprite>("PieceSprite");

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);

            Add(_pieceSprite);

            SubscribeToEvents();
            RefreshNextPieceSprite();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            UnsubscribeFromEvents();
        }

        public void Inject([NotNull] IBagView bagView)
        {
            ArgumentNullException.ThrowIfNull(bagView);

            _bagView = bagView;
        }

        private void SubscribeToEvents()
        {
            InvalidOperationException.ThrowIfNull(_bagView);

            UnsubscribeFromEvents();

            _bagView.OnUpdated += RefreshNextPieceSprite;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_bagView);

            _bagView.OnUpdated -= RefreshNextPieceSprite;
        }

        private void RefreshNextPieceSprite()
        {
            InvalidOperationException.ThrowIfNull(_pieceSpriteContainer);
            InvalidOperationException.ThrowIfNull(_bagView);

            PieceType? next = _bagView.Next;
            Sprite nextSprite = next.HasValue ? _pieceSpriteContainer.Get(next.Value) : null;

            _pieceSprite.Value = nextSprite;
        }
    }
}