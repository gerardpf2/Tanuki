using System;
using Game.Gameplay.EventEnqueueing.Events;
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

            // TODO
            // 1) Move to board position
            // 2) Despawn player
            // 3) Spawn board

            _actionFactory.GetDestroyPlayerPieceAction().Resolve(onComplete);
        }
    }
}