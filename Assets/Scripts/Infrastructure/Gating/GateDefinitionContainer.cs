using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.Gating
{
    [CreateAssetMenu(fileName = nameof(GateDefinitionContainer), menuName = "Tanuki/Infrastructure/Gating/" + nameof(GateDefinitionContainer))]
    public class GateDefinitionContainer : ScriptableObject, IGateDefinitionGetter
    {
        [SerializeField] private GateDefinition[] _gateDefinitions;

        public IGateDefinition Get(string gateKey)
        {
            InvalidOperationException.ThrowIfNull(_gateDefinitions);

            IGateDefinition gateDefinition = null;

            foreach (GateDefinition gateDefinitionCandidate in _gateDefinitions)
            {
                InvalidOperationException.ThrowIfNull(gateDefinitionCandidate);

                if (gateDefinitionCandidate.GateKey != gateKey)
                {
                    continue;
                }

                gateDefinition = gateDefinitionCandidate;

                break;
            }

            InvalidOperationException.ThrowIfNullWithMessage(
                gateDefinition,
                $"Cannot get gate definition with GateKey: {gateKey}"
            );

            return gateDefinition;
        }
    }
}