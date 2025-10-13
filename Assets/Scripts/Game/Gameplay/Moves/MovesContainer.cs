using Game.Common;

namespace Game.Gameplay.Moves
{
    public class MovesContainer : IMovesContainer
    {
        private InitializedLabel _initializedLabel;

        public IMoves Moves { get; private set; }

        public void Initialize(IMoves moves)
        {
            _initializedLabel.SetInitialized();

            Moves = moves;
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            Moves = null;
        }
    }
}