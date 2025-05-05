using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public interface IPlayerView
    {
        void Initialize();

        [NotNull]
        GameObject Instantiate(IPiece piece, GameObject prefab);
    }
}