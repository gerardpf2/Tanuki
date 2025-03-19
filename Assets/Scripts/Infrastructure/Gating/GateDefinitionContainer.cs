using System.Collections.Generic;
using Infrastructure.System.Exceptions;
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

            if (gateDefinition is null)
            {
                InvalidOperationException.Throw($"Cannot get gate definition with GateKey: {gateKey}");
            }

            return gateDefinition;
        }
    }
}