using System.Diagnostics.CodeAnalysis;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public class PlayerView : IPlayerView
    {
        private Transform _playerPieceParent;

        public void Initialize()
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            _playerPieceParent = new GameObject("PlayerPieceParent").transform; // New game object outside canvas, etc
        }

        public GameObject Instantiate([NotNull] IPiece piece, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(_playerPieceParent);

            GameObject instance = Object.Instantiate(prefab, _playerPieceParent);

            InvalidOperationException.ThrowIfNullWithMessage(
                instance,
                $"Cannot instantiate player piece with Prefab: {prefab.name}"
            );

            return instance;
        }
    }
}