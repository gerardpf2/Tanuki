using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;
using Random = UnityEngine.Random;

namespace Infrastructure.Unity.Animator
{
    public class SetTriggerWeightedOnStateEnter : StateMachineBehaviour
    {
        [Serializable]
        private struct TriggerNameWeightPair
        {
            [SerializeField] private string _triggerName;
            [SerializeField, Min(1)] private int _weight;

            public string TriggerName => _triggerName;

            public int Weight => _weight;
        }

        [SerializeField] private TriggerNameWeightPair[] _triggerNameWeightPairs;

        public override void OnStateEnter(
            [NotNull] UnityEngine.Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            ArgumentNullException.ThrowIfNull(animator);

            base.OnStateEnter(animator, stateInfo, layerIndex);

            animator.SetTrigger(GetTriggerName());
        }

        private string GetTriggerName()
        {
            IReadOnlyList<KeyValuePair<string, int>> accumulatedWeightsPerTriggerName = GetAccumulatedWeightsPerTriggerName();

            if (accumulatedWeightsPerTriggerName.Count > 0)
            {
                const int min = 0;
                int max = accumulatedWeightsPerTriggerName[^1].Value;
                int random = Random.Range(min, max);

                foreach ((string triggerName, int weight) in accumulatedWeightsPerTriggerName)
                {
                    if (random < weight)
                    {
                        return triggerName;
                    }
                }
            }

            return null;
        }

        [NotNull]
        private IReadOnlyList<KeyValuePair<string, int>> GetAccumulatedWeightsPerTriggerName()
        {
            InvalidOperationException.ThrowIfNull(_triggerNameWeightPairs);

            List<KeyValuePair<string, int>> accumulatedWeightsPerTriggerName = new();
            int accumulatedWeight = 0;

            foreach (TriggerNameWeightPair triggerNameWeightPair in _triggerNameWeightPairs)
            {
                accumulatedWeight += triggerNameWeightPair.Weight;

                accumulatedWeightsPerTriggerName.Add(
                    new KeyValuePair<string, int>(
                        triggerNameWeightPair.TriggerName,
                        accumulatedWeight
                    )
                );
            }

            return accumulatedWeightsPerTriggerName;
        }
    }
}