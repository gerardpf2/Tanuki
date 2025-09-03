using System;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class LockPlayerPieceEventResolver : IEventResolver<LockPlayerPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public LockPlayerPieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        public void Resolve([NotNull] LockPlayerPieceEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            // TODO: Move to board position

            DestroyPlayerPieceStep(() => InstantiatePieceStep(onComplete));

            return;

            void DestroyPlayerPieceStep(Action onStepComplete)
            {
                _actionFactory.GetDestroyPlayerPieceAction(DestroyPieceReason.Lock).Resolve(onStepComplete);
            }

            void InstantiatePieceStep(Action onStepComplete)
            {
                _actionFactory
                    .GetInstantiatePieceAction(evt.Piece, InstantiatePieceReason.Lock, evt.LockSourceCoordinate)
                    .Resolve(onStepComplete);
            }
        }
    }
}