using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Gating
{
    [CreateAssetMenu(fileName = nameof(GateDefinitionContainer), menuName = "Tanuki/Infrastructure/Gating/" + nameof(GateDefinitionContainer))]
    public class GateDefinitionContainer : ScriptableObject, IGateDefinitionGetter
    {
        [SerializeField] private List<GateDefinition> _gateDefinitions = new();

        public GateDefinition Get(string key)
        {
            GateDefinition gateDefinition = _gateDefinitions.Find(gateDefinition => gateDefinition.Key == key);

            if (gateDefinition == null)
            {
                throw new InvalidOperationException($"Cannot get gate definition with Key: {key}");
            }

            return gateDefinition;
        }
    }
}