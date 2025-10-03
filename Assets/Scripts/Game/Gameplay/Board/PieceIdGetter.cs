namespace Game.Gameplay.Board
{
    public class PieceIdGetter : IPieceIdGetter
    {
        private int _id;

        public void Initialize()
        {
            Uninitialize();

            Reset();
        }

        public void Uninitialize()
        {
            Reset();
        }

        public int GetNew()
        {
            return _id++;
        }

        private void Reset()
        {
            _id = 0;
        }
    }
}