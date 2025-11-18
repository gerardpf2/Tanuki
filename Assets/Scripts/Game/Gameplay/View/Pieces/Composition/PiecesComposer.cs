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
                    new DecomposePiecesGameObjectPreloader(
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IGameObjectPool>(),
                        r.Resolve<IBoard>()
                    )
                ),
                PiecesComposerKeys.Preloader.DecomposePieces
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceGameObjectPreloader>(r =>
                    new PieceGameObjectPreloaderGroup(
                        r.Resolve<IPieceGameObjectPreloader>(PiecesComposerKeys.Preloader.BoardPieces),
                        r.Resolve<IPieceGameObjectPreloader>(PiecesComposerKeys.Preloader.PieceGhosts),
                        r.Resolve<IPieceGameObjectPreloader>(PiecesComposerKeys.Preloader.DecomposePieces)
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceGameObjectPreloader>(r =>
                    new PieceGhostsGameObjectPreloader(
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IGameObjectPool>(),
                        r.Resolve<IBag>()
                    )
                ),
                PiecesComposerKeys.Preloader.PieceGhosts
            );

            ruleAdder.Add(ruleFactory.GetInstance(_pieceViewDefinitionGetter));
        }
    }
}