using Game.Gameplay.Board;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    // TODO: Remove if not needed
    public interface IBoardPositionGetter
    {
        Vector3 Get(Coordinate coordinate);
    }
}