using System.Collections.Generic;
using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals
{
    public class GoalsContainer : IGoalsContainer
    {
        [NotNull] private readonly IDictionary<PieceType, int> _initialAmounts = new Dictionary<PieceType, int>();
        [NotNull] private readonly IDictionary<PieceType, int> _currentAmounts = new Dictionary<PieceType, int>();

        public IEnumerable<PieceType> PieceTypes => _initialAmounts.Keys;

        public void Initialize([NotNull, ItemNotNull] IEnumerable<IGoalDefinition> initialGoalDefinitions)
        {
            ArgumentNullException.ThrowIfNull(initialGoalDefinitions);

            Uninitialize();

            foreach (IGoalDefinition goalDefinition in initialGoalDefinitions)
            {
                ArgumentNullException.ThrowIfNull(goalDefinition);

                _initialAmounts.Add(goalDefinition.PieceType, goalDefinition.Amount);
                _currentAmounts.Add(goalDefinition.PieceType, 0);
            }
        }

        public void Uninitialize()
        {
            _initialAmounts.Clear();
            _currentAmounts.Clear();
        }

        public int GetInitialAmount(PieceType pieceType)
        {
            return GetAmount(pieceType, _initialAmounts);
        }

        public int GetCurrentAmount(PieceType pieceType)
        {
            return GetAmount(pieceType, _currentAmounts);
        }

        public void TryRegisterDestroyed(PieceType pieceType)
        {
            if (!_currentAmounts.ContainsKey(pieceType))
            {
                return;
            }

            ++_currentAmounts[pieceType];

            HandleCurrentUpdated(pieceType);
        }

        private static int GetAmount(PieceType pieceType, IDictionary<PieceType, int> amounts)
        {
            if (!amounts.TryGetValue(pieceType, out int amount))
            {
                InvalidOperationException.Throw(
                    $"Piece type {pieceType} cannot be found. Make sure it is part of the goals"
                );
            }

            return amount;
        }

        protected virtual void HandleCurrentUpdated(PieceType pieceType) { }
    }
}