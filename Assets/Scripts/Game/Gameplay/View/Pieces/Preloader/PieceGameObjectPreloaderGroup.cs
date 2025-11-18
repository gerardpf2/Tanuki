using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Pieces.Preloader
{
    public class PieceGameObjectPreloaderGroup : IPieceGameObjectPreloader
    {
        [NotNull, ItemNotNull] private readonly IReadOnlyCollection<IPieceGameObjectPreloader> _pieceGameObjectPreloaders;

        public PieceGameObjectPreloaderGroup(
            [NotNull, ItemNotNull] params IPieceGameObjectPreloader[] pieceGameObjectPreloaders)
        {
            ArgumentNullException.ThrowIfNull(pieceGameObjectPreloaders);

            List<IPieceGameObjectPreloader> pieceGameObjectPreloadersCopy = new();

            foreach (IPieceGameObjectPreloader pieceGameObjectPreloader in pieceGameObjectPreloaders)
            {
                ArgumentNullException.ThrowIfNull(pieceGameObjectPreloader);

                pieceGameObjectPreloadersCopy.Add(pieceGameObjectPreloader);
            }

            _pieceGameObjectPreloaders = pieceGameObjectPreloadersCopy;
        }

        public void Preload()
        {
            foreach (IPieceGameObjectPreloader pieceGameObjectPreloader in _pieceGameObjectPreloaders)
            {
                pieceGameObjectPreloader.Preload();
            }
        }
    }
}