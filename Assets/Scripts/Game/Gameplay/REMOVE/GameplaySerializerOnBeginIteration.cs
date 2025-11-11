using Game.Common;
using Game.Gameplay.Bag;
using Game.Gameplay.Goals;
using Game.Gameplay.Parsing;
using Game.Gameplay.Phases;
using Infrastructure.Logging;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.REMOVE
{
    public class GameplaySerializerOnBeginIteration : IGameplaySerializerOnBeginIteration
    {
        [NotNull] private readonly IBagContainer _bagContainer;
        [NotNull] private readonly IGoalsContainer _goalsContainer;
        [NotNull] private readonly IGameplayParser _gameplayParser;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly ILogger _logger;

        private InitializedLabel _initializedLabel;

        public GameplaySerializerOnBeginIteration(
            [NotNull] IBagContainer bagContainer,
            [NotNull] IGoalsContainer goalsContainer,
            [NotNull] IGameplayParser gameplayParser,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(bagContainer);
            ArgumentNullException.ThrowIfNull(goalsContainer);
            ArgumentNullException.ThrowIfNull(gameplayParser);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(logger);

            _bagContainer = bagContainer;
            _goalsContainer = goalsContainer;
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

        private void HandleBeginIteration()
        {
            string serializedGameplay = _gameplayParser.Serialize(_goalsContainer.Goals, _bagContainer.Bag);

            _logger.Info(serializedGameplay);
        }
    }
}