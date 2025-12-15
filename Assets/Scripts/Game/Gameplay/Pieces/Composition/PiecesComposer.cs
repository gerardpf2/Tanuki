using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using Game.Gameplay.Pieces.Parsing;
using Infrastructure.DependencyInjection;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Composition
{
    public class PiecesComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPiecePlacementSerializedDataConverter>(r =>
                    new PiecePlacementSerializedDataConverter(
                        r.Resolve<IPieceSerializedDataConverter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceSerializedDataConverter>(r =>
                    new PieceSerializedDataConverter(
                        r.Resolve<IPieceGetter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IDamagePieceHelper>(r =>
                    new DamagePieceHelper(
                        r.Resolve<IBoard>(),
                        r.Resolve<IGoals>(),
                        r.Resolve<IPieceGetter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceFactory>(r =>
                    new PieceFactory(
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<IConverter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceGetter>(r =>
                    new PieceGetter(
                        r.Resolve<IPieceFactory>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IPieceIdGetter>(_ => new PieceIdGetter()));
        }
    }
}