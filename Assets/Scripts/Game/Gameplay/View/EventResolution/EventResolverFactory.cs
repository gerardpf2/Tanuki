using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.EventResolution.EventResolvers;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
{
    public class EventResolverFactory : IEventResolverFactory
    {
        // TODO: Reuse instead of new ¿?

        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IBoardViewController _boardViewController;

        public EventResolverFactory(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IBoardViewController boardViewController)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardViewController);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _boardViewController = boardViewController;
        }

        public IEventResolver<InstantiateEvent> GetInstantiate()
        {
            return new InstantiateEventResolver(_pieceViewDefinitionGetter, _boardViewController);
        }

        public IEventResolver<InstantiatePlayerPieceEvent> GetInstantiatePlayerPiece()
        {
            return new InstantiatePlayerPieceEventResolver();
        }
    }
}