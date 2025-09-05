using Game.Root.UseCases;
using Infrastructure.Configuring;
using Infrastructure.DependencyInjection;
using Infrastructure.Gating;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using UnityEngine;

namespace Game.Root
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private GateDefinitionContainer _gateDefinitionContainer;
        [SerializeField] private ConfigDefinitionContainer _configDefinitionContainer;
        [SerializeField] private ScreenDefinitionContainer _screenDefinitionContainer;
        [SerializeField] private RootScreenPlacement _rootScreenPlacement;
        [SerializeField] private CoroutineRunner _coroutineRunner;

        private Scope _root; // TODO: Check if storing ref is needed

        private void Awake()
        {
            InvalidOperationException.ThrowIfNull(_gateDefinitionContainer);
            InvalidOperationException.ThrowIfNull(_configDefinitionContainer);
            InvalidOperationException.ThrowIfNull(_screenDefinitionContainer);
            InvalidOperationException.ThrowIfNull(_rootScreenPlacement);
            InvalidOperationException.ThrowIfNull(_coroutineRunner);

            IBuildAndInitializeRootScopeUseCase buildAndInitializeRootScopeUseCase =
                new BuildAndInitializeRootScopeUseCase(
                    _gateDefinitionContainer,
                    _configDefinitionContainer,
                    _screenDefinitionContainer,
                    _rootScreenPlacement,
                    _coroutineRunner
                );

            _root = buildAndInitializeRootScopeUseCase.Resolve();
        }
    }
}