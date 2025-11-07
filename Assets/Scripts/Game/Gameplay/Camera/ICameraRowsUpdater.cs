namespace Game.Gameplay.Camera
{
    public interface ICameraRowsUpdater
    {
        void TargetHighestNonEmptyRow();

        void TargetLockRow(int lockRow);
    }
}