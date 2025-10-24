using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Game.Gameplay.Parsing;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.REMOVE
{
    public class GameplaySerialize : MonoBehaviour
    {
        private IBagContainer _bagContainer;
        private IBoardContainer _boardContainer;
        private IGoalsContainer _goalsContainer;
        private IMovesContainer _movesContainer;
        private IGameplayParser _gameplayParser;

        private void Start()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] IBagContainer bagContainer,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IGoalsContainer goalsContainer,
            [NotNull] IMovesContainer movesContainer,
            [NotNull] IGameplayParser gameplayParser)
        {
            ArgumentNullException.ThrowIfNull(bagContainer);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(goalsContainer);
            ArgumentNullException.ThrowIfNull(movesContainer);
            ArgumentNullException.ThrowIfNull(gameplayParser);

            _bagContainer = bagContainer;
            _boardContainer = boardContainer;
            _goalsContainer = goalsContainer;
            _movesContainer = movesContainer;
            _gameplayParser = gameplayParser;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SerializeAndLog();
            }
        }

        private void SerializeAndLog()
        {
            InvalidOperationException.ThrowIfNull(_bagContainer);
            InvalidOperationException.ThrowIfNull(_boardContainer);
            InvalidOperationException.ThrowIfNull(_goalsContainer);
            InvalidOperationException.ThrowIfNull(_movesContainer);
            InvalidOperationException.ThrowIfNull(_gameplayParser);

            IBag bag = _bagContainer.Bag;
            IBoard board = _boardContainer.Board;
            IGoals goals = _goalsContainer.Goals;
            IMoves moves = _movesContainer.Moves;

            string serializedGameplay = _gameplayParser.Serialize(board, goals, moves, bag);

            Debug.Log(serializedGameplay);
        }
    }
}