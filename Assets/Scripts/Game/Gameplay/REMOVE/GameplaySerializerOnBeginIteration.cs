using Game.Common;
using Game.Gameplay.Parsing;
using Game.Gameplay.Phases;
using Infrastructure.Logging;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.REMOVE
{
    public class GameplaySerializerOnBeginIteration : IGameplaySerializerOnBeginIteration
    {
        [NotNull] private readonly IGameplayParser _gameplayParser;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly ILogger _logger;

        private InitializedLabel _initializedLabel;

        public GameplaySerializerOnBeginIteration(
            [NotNull] IGameplayParser gameplayParser,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(gameplayParser);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(logger);

            _gameplayParser = gameplayParser;
            _phaseResolver = phaseResolver;
            _logger = logger;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            SubscribeToEvents();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _phaseResolver.OnBeginIteration += HandleBeginIteration;
        }

        private void UnsubscribeFromEvents()
        {
            _phaseResolver.OnBeginIteration -= HandleBeginIteration;
        }

        private void HandleBeginIteration([NotNull] ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            if (resolveContext.ResolveReason is not ResolveReason.Lock)
            {
                return;
            }

            string serializedGameplay = _gameplayParser.Serialize();

            _logger.Info(serializedGameplay);
        }
    }
}