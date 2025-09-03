using System.Collections.Generic;

namespace Game.Gameplay.Board.Pieces
{
    public interface IPieceUpdater
    {
        void ProcessCustomData(IEnumerable<KeyValuePair<string, string>> customData);

        void Damage(int rowOffset, int columnOffset);
    }
}