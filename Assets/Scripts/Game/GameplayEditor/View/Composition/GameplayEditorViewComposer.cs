using Game.Gameplay;
using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Game.Gameplay.Parsing;
using Game.Gameplay.Phases;
using Game.Gameplay.Pieces;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Game.Gameplay.View.Player;
using Game.Gameplay.View.Player.Composition;
using Game.Gameplay.View.Player.Input.ActionHandlers;
using Game.GameplayEditor.Phases.Composition;
using Game.GameplayEditor.View.REMOVE;
using Game.GameplayEditor.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.GameplayEditor.View.Composition
{
    public class GameplayEditorViewComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            // Not shared so it can only be unloaded from here
            ruleAdder.Add(
                ruleFactory.GetSingleton<IUnloadGameplayEditorUseCase>(r =>
                    new UnloadGameplayEditorUseCase(
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IGoals>(),
                        r.Resolve<IMoves>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IMovesView>(),
                        r.Resolve<IPlayerInputActionHandler>(PlayerComposerKeys.PlayerInputActionHandler.Lock),
                        r.Resolve<IPlayerInputActionHandler>(PlayerComposerKeys.PlayerInputActionHandler.MoveLeft),
                        r.Resolve<IPlayerInputActionHandler>(PlayerComposerKeys.PlayerInputActionHandler.MoveRight),
                        r.Resolve<IPlayerInputActionHandler>(PlayerComposerKeys.PlayerInputActionHandler.Rotate),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IScreenLoader>()
                    )
                )
            );
        }

        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetInject<UnloadGameplayEditor>((r, s) =>
                    s.Inject(
                        r.Resolve<IUnloadGameplayEditorUseCase>()
                    )
                )
            );

            // Shared so it can be loaded from anywhere
            ruleAdder.Add(
                ruleFactory.GetSingleton<ILoadGameplayEditorUseCase>(r =>
                    new LoadGameplayEditorUseCase(
                        r.Resolve<IGameplayDefinitionGetter>(),
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IGameplayParser>(),
                        r.Resolve<IPhaseContainer>(PhasesEditorComposerKeys.PhaseContainer.InitialEditor),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IMovesView>(),
                        r.Resolve<IPlayerInputActionHandler>(PlayerComposerKeys.PlayerInputActionHandler.Lock),
                        r.Resolve<IPlayerInputActionHandler>(PlayerComposerKeys.PlayerInputActionHandler.MoveLeft),
                        r.Resolve<IPlayerInputActionHandler>(PlayerComposerKeys.PlayerInputActionHandler.MoveRight),
                        r.Resolve<IPlayerInputActionHandler>(PlayerComposerKeys.PlayerInputActionHandler.Rotate),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IScreenLoader>()
                    )
                )
            );
        }
    }
}