using Game.Common;

namespace Game.Gameplay.Board
{
    public class PieceIdGetter : IPieceIdGetter
    {
        private InitializedLabel _initializedLabel;

        private int _id;

        public void Initialize()
        {
            _initializedLabel.SetInitialized();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            _id = 0;
        }

        public int GetNew()
        {
            return _id++;
        }
    }
}