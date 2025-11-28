namespace Game.Gameplay.Events.Events
{
    public class DestroyPieceData
    {
        public readonly UpdateGoalData UpdateGoalData;
        public readonly DecomposePieceData DecomposePieceData;

        public DestroyPieceData(UpdateGoalData updateGoalData, DecomposePieceData decomposePieceData)
        {
            UpdateGoalData = updateGoalData;
            DecomposePieceData = decomposePieceData;
        }
    }
}