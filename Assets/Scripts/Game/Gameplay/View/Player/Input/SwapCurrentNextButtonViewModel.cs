using Game.Common.Pieces;
using Game.Common.View.Pieces;
using Game.Gameplay.Bag;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.EventResolvers;
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

        private IBag _bag;
        private IEventsResolver _eventsResolver;

        [NotNull] private readonly IBoundProperty<Sprite> _pieceSprite = new BoundProperty<Sprite>("PieceSprite");

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);

            Add(_pieceSprite);

            SubscribeToEvents();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            UnsubscribeFromEvents();
        }

        public void Inject([NotNull] IBag bag, [NotNull] IEventsResolver eventsResolver)
        {
            ArgumentNullException.ThrowIfNull(bag);
            ArgumentNullException.ThrowIfNull(eventsResolver);

            _bag = bag;
            _eventsResolver = eventsResolver;
        }

        private void SubscribeToEvents()
        {
            InvalidOperationException.ThrowIfNull(_eventsResolver);

            UnsubscribeFromEvents();

            _eventsResolver.OnResolveEnd += HandleResolveEnd;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_eventsResolver);

            _eventsResolver.OnResolveEnd -= HandleResolveEnd;
        }

        private void HandleResolveEnd()
        {
            InvalidOperationException.ThrowIfNull(_pieceSpriteContainer);
            InvalidOperationException.ThrowIfNull(_bag);

            IPiece next = _bag.Next;
            PieceType nextType = next.Type;
            Sprite nextSprite = _pieceSpriteContainer.Get(nextType);

            _pieceSprite.Value = nextSprite;
        }
    }
}