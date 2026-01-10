using System;
using Game.Common.Pieces;
using Game.Gameplay.View.Bag;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Actions.Actions
{
    public class SetBagNextAction : IAction
    {
        [NotNull] private readonly IBagView _bagView;
        private readonly PieceType _pieceType;

        public SetBagNextAction([NotNull] IBagView bagView, PieceType pieceType)
        {
            ArgumentNullException.ThrowIfNull(bagView);

            _bagView = bagView;
            _pieceType = pieceType;
        }

        public void Resolve(Action onComplete)
        {
            _bagView.Next = _pieceType;

            onComplete?.Invoke();
        }
    }
}