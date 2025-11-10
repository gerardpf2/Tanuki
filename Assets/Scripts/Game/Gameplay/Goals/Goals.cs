using System.Collections.Generic;
using System.Linq;
using Game.Common.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals
{
    public class Goals : IGoals
    {
        [NotNull] private readonly IDictionary<PieceType, IGoal> _goals = new Dictionary<PieceType, IGoal>();

        public IEnumerable<PieceType> PieceTypes => _goals.Keys;

        // TODO: Remove
        public Goals([NotNull, ItemNotNull] IEnumerable<IGoal> goals)
        {
            ArgumentNullException.ThrowIfNull(goals);

            Dictionary<PieceType, IGoal> goalsCopy = new();

            foreach (IGoal goal in goals)
            {
                ArgumentNullException.ThrowIfNull(goal);

                if (!goalsCopy.TryAdd(goal.PieceType, goal))
                {
                    InvalidOperationException.Throw($"Cannot add goal with PieceType: {goal.PieceType}");
                }
            }

            _goals = goalsCopy;
        }

        public void Add([NotNull] IGoal goal)
        {
            ArgumentNullException.ThrowIfNull(goal);

            if (!_goals.TryAdd(goal.PieceType, goal))
            {
                InvalidOperationException.Throw($"Cannot add goal with PieceType: {goal.PieceType}");
            }
        }

        public IGoal Get(PieceType pieceType)
        {
            if (!TryGet(pieceType, out IGoal goal))
            {
                InvalidOperationException.Throw($"Cannot find goal with PieceType: {pieceType}");
            }

            return goal;
        }

        public bool TryGet(PieceType pieceType, out IGoal goal)
        {
            return _goals.TryGetValue(pieceType, out goal);
        }

        public void Clear()
        {
            _goals.Clear();
        }

        public IGoals Clone()
        {
            return new Goals(PieceTypes.Select(pieceType => Get(pieceType).Clone()));
        }
    }
}