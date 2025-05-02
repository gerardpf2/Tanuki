using Game.Gameplay.Board;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardPositionGetter : IBoardPositionGetter
    {
        public Vector3 Get(Coordinate coordinate)
        {
            return new Vector3(coordinate.Column, coordinate.Row);
        }
    }
}