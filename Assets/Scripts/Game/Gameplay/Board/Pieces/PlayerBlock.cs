using System.Collections.Generic;
using Infrastructure.System;
using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerBlock : IPiece, IPieceUpdater
    {
        /*
         *
         * 1 Row x 1 Column
         *
         * Has no special behaviour
         *
         */

        public bool Alive { get; private set; } = true;

        public IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate)
        {
            yield return sourceCoordinate;
        }

        public void Damage(
            [Is(ComparisonOperator.EqualTo, 0)] int rowOffset,
            [Is(ComparisonOperator.EqualTo, 0)] int columnOffset)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rowOffset, ComparisonOperator.EqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(columnOffset, ComparisonOperator.EqualTo, 0);

            Alive = false;
        }
    }
}