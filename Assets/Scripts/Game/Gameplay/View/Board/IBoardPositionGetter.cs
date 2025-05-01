using Game.Gameplay.Board;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardPositionGetter
    {
        Vector3 Get(Coordinate coordinate);
    }
}