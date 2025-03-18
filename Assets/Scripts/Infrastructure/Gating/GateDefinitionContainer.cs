using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Gating
{
    [CreateAssetMenu(fileName = nameof(GateDefinitionContainer), menuName = "Tanuki/Infrastructure/Gating/" + nameof(GateDefinitionContainer))]
    public class GateDefinitionContainer : ScriptableObject, IGateDefinitionGetter
    {
        [NotNull] [SerializeField] private List<GateDefinition> _gateDefinitions = new();

        public IGateDefinition Get(string gateKey)
        {
            IGateDefinition gateDefinition = _gateDefinitions.Find(gateDefinition => gateDefinition.GateKey == gateKey);

            if (gateDefinition == null)
            {
                throw new InvalidOperationException($"Cannot get gate definition with GateKey: {gateKey}");
            }

            return gateDefinition;
        }
    }
}