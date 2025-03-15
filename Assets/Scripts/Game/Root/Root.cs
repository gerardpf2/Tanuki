using Game.Root.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.Gating;
using Infrastructure.ScreenLoading;
using UnityEngine;

namespace Game.Root
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private GateDefinitionContainer _gateDefinitionContainer;
        [SerializeField] private ScreenDefinitionContainer _screenDefinitionContainer;
        [SerializeField] private RootScreenPlacement _rootScreenPlacement;

        private Scope _root; // TODO: Check if storing ref is needed

        private void Awake()
        {
            IBuildAndInitializeRootScopeUseCase buildAndInitializeRootScopeUseCase =
                new BuildAndInitializeRootScopeUseCase(
                    _gateDefinitionContainer,
                    _screenDefinitionContainer,
                    _rootScreenPlacement
                );

            _root = buildAndInitializeRootScopeUseCase.Resolve();
        }
    }
}