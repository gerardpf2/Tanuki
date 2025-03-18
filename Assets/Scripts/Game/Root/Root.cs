using Game.Root.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.Gating;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
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
            InvalidOperationException.ThrowIfNull(_gateDefinitionContainer);
            InvalidOperationException.ThrowIfNull(_screenDefinitionContainer);
            InvalidOperationException.ThrowIfNull(_rootScreenPlacement);

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