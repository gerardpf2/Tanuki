using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class DamagePieceEventResolver : IEventResolver<DamagePieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public DamagePieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        public void Resolve([NotNull] DamagePieceEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _actionFactory.GetDamagePieceAction(evt.Piece).Resolve(onComplete);
        }
    }
}