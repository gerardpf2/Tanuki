namespace Game.Gameplay.Camera
{
    public interface ICameraRowsUpdater
    {
        int TargetHighestNonEmptyRow();

        int TargetLockRow(int lockRow);
    }
}