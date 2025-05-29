using System;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.View.Board;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class DamagePieceAction : IAction
    {
        [NotNull] private readonly IPiece _piece;
        [NotNull] private readonly IReadonlyBoardView _boardView;

        public DamagePieceAction([NotNull] IPiece piece, [NotNull] IReadonlyBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(boardView);

            _piece = piece;
            _boardView = boardView;
        }

        public void Resolve(Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }
    }
}