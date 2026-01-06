using Game;
using Infrastructure.Configuring;
using Infrastructure.DependencyInjection;
using Infrastructure.Gating;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using Root.UseCases;
using UnityEngine;

namespace Root
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private GateDefinitionContainer _gateDefinitionContainer;
        [SerializeField] private ConfigDefinitionContainer _configDefinitionContainer;
        [SerializeField] private ScreenContainer _screenContainer;
        [SerializeField] private RootScreenPlacement _rootScreenPlacement;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private GameScopeComposerBuilder _gameScopeComposerBuilder;

        private Scope _root; // TODO: Check if storing ref is needed

        private void Awake()
        {
            InvalidOperationException.ThrowIfNull(_gateDefinitionContainer);
            InvalidOperationException.ThrowIfNull(_configDefinitionContainer);
            InvalidOperationException.ThrowIfNull(_screenContainer);
            InvalidOperationException.ThrowIfNull(_rootScreenPlacement);
            InvalidOperationException.ThrowIfNull(_coroutineRunner);
            InvalidOperationException.ThrowIfNull(_gameScopeComposerBuilder);

            IBuildAndInitializeRootScopeUseCase buildAndInitializeRootScopeUseCase =
                new BuildAndInitializeRootScopeUseCase(
                    _gateDefinitionContainer,
                    _configDefinitionContainer,
                    _screenContainer,
                    _rootScreenPlacement,
                    _coroutineRunner,
                    _gameScopeComposerBuilder
                );

            _root = buildAndInitializeRootScopeUseCase.Resolve();
        }
    }
}