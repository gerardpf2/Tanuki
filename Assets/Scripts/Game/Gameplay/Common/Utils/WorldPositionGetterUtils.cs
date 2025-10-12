using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.Common.Utils
{
    public static class WorldPositionGetterUtils
    {
        public static Vector3 Get([NotNull] this IWorldPositionGetter worldPositionGetter, int row, int column)
        {
            ArgumentNullException.ThrowIfNull(worldPositionGetter);

            return
                new Vector3(
                    worldPositionGetter.GetX(column),
                    worldPositionGetter.GetY(row),
                    worldPositionGetter.GetZ()
                );
        }

        public static Vector3 Get([NotNull] this IWorldPositionGetter worldPositionGetter, Coordinate coordinate)
        {
            ArgumentNullException.ThrowIfNull(worldPositionGetter);

            return worldPositionGetter.Get(coordinate.Row, coordinate.Column);
        }
    }
}