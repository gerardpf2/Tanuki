using System;
using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using JetBrains.Annotations;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay
{
    [Serializable]
    public class GameplayDefinition : IGameplayDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private BoardDefinition _boardDefinition;
        [NotNull, SerializeField] private List<GoalDefinition> _goalDefinitions = new();

        public string Id => _id;

        public IBoardDefinition BoardDefinition
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_boardDefinition);

                return _boardDefinition;
            }
        }

        public IEnumerable<IGoalDefinition> GoalDefinitions
        {
            get
            {
                foreach (GoalDefinition goalDefinition in _goalDefinitions)
                {
                    InvalidOperationException.ThrowIfNull(goalDefinition);
                }

                return _goalDefinitions;
            }
        }
    }
}