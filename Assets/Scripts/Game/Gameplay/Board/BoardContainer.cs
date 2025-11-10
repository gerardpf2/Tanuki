using Game.Common;

namespace Game.Gameplay.Board
{
    public class BoardContainer : IBoardContainer
    {
        private InitializedLabel _initializedLabel;

        public IBoard Board { get; private set; }

        public void Initialize(IBoard board)
        {
            _initializedLabel.SetInitialized();

            Board = board;
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            Board = null;
        }
    }
}