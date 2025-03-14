using Game.Root.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.Gating;
using UnityEngine;

namespace Game.Root
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private GateDefinitionContainer _gateDefinitionContainer;

        private Scope _root; // TODO: Check if storing ref is needed

        private void Awake()
        {
            IBuildAndInitializeRootScopeUseCase buildAndInitializeRootScopeUseCase = new BuildAndInitializeRootScopeUseCase(_gateDefinitionContainer);
            _root = buildAndInitializeRootScopeUseCase.Resolve();
        }
    }
}