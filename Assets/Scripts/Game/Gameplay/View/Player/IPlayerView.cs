using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public interface IPlayerView
    {
        Coordinate Coordinate { get; }

        void Initialize();

        [NotNull]
        GameObject Instantiate(IPiece piece, GameObject prefab);

        void Move(float deltaX);
    }
}