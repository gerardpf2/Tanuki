using System;
using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay
{
    [Serializable]
    public class GameplayDefinition : IGameplayDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private BoardDefinition _boardDefinition;
        [SerializeField] private GoalsDefinition _goalsDefinition;
        [SerializeField] private GoalDefinition[] _goalDefinitions;

        public string Id => _id;

        public IBoardDefinition BoardDefinition
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_boardDefinition);

                return _boardDefinition;
            }
        }

        public IGoalsDefinition GoalsDefinition
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_goalsDefinition);

                return _goalsDefinition;
            }
        }

        public IEnumerable<IGoalDefinition> GoalDefinitions
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_goalDefinitions);

                foreach (GoalDefinition goalDefinition in _goalDefinitions)
                {
                    InvalidOperationException.ThrowIfNull(goalDefinition);
                }

                return _goalDefinitions;
            }
        }
    }
}