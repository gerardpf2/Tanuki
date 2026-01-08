using System;

namespace Game.Gameplay.View.UseCases
{
    public interface ILoadGameplayUseCase
    {
        void Resolve(Action onReady);
    }
}