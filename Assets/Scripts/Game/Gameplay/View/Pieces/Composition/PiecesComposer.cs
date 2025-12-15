using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.View.Pieces.Preloader;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Pieces.Composition
{
    public class PiecesComposer : ScopeComposer
    {
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;

        public PiecesComposer([NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceGameObjectPreloader>(r =>
                    new BoardPiecesGameObjectPreloader(
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IGameObjectPool>(),
                        r.Resolve<IBoard>()
                    )
                ),
                PiecesComposerKeys.Preloader.BoardPieces
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceGameObjectPreloader>(r =>
                    new DecomposeBoardPiecesGameObjectPreloader(
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IGameObjectPool>(),
                        r.Resolve<IBoard>()
                    )
                ),
                PiecesComposerKeys.Preloader.DecomposeBoardPieces
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceGameObjectPreloader>(r =>
                    new PieceGameObjectPreloaderGroup(
                        r.Resolve<IPieceGameObjectPreloader>(PiecesComposerKeys.Preloader.BoardPieces),
                        r.Resolve<IPieceGameObjectPreloader>(PiecesComposerKeys.Preloader.PlayerPieces),
                        r.Resolve<IPieceGameObjectPreloader>(PiecesComposerKeys.Preloader.PlayerPieceGhosts),
                        r.Resolve<IPieceGameObjectPreloader>(PiecesComposerKeys.Preloader.DecomposeBoardPieces)
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceGameObjectPreloader>(r =>
                    new PlayerPieceGhostsGameObjectPreloader(
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IGameObjectPool>(),
                        r.Resolve<IBag>()
                    )
                ),
                PiecesComposerKeys.Preloader.PlayerPieceGhosts
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceGameObjectPreloader>(r =>
                    new PlayerPiecesGameObjectPreloader(
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IGameObjectPool>(),
                        r.Resolve<IBag>()
                    )
                ),
                PiecesComposerKeys.Preloader.PlayerPieces
            );

            ruleAdder.Add(ruleFactory.GetInstance(_pieceViewDefinitionGetter));
        }
    }
}