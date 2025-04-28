using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.EventResolution.EventResolvers;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
{
    public class EventResolverFactory : IEventResolverFactory
    {
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IBoardView _boardView;

        public EventResolverFactory(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardView);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _boardView = boardView;
        }

        public IEventResolver<InstantiateEvent> GetInstantiate()
        {
            return new InstantiateEventResolver(_pieceViewDefinitionGetter, _boardView);
        }
    }
}