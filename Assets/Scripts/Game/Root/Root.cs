using Game.Root.UseCases;
using Infrastructure.DependencyInjection;
using UnityEngine;

namespace Game.Root
{
    public class Root : MonoBehaviour
    {
        private Scope _root; // TODO: Check if storing ref is needed

        private void Awake()
        {
            IBuildAndInitializeRootScopeUseCase buildAndInitializeRootScopeUseCase = new BuildAndInitializeRootScopeUseCase();
            _root = buildAndInitializeRootScopeUseCase.Resolve();
        }
    }
}