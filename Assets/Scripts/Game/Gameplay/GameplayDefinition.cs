using System;
using System.Collections.Generic;
using Game.Gameplay.Goals;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay
{
    [Serializable]
    public class GameplayDefinition : IGameplayDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private string _board;
        [SerializeField] private string _goals;
        [SerializeField] private GoalDefinition[] _goalDefinitions;

        public string Id => _id;

        public string Board => _board;

        public string Goals => _goals;

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