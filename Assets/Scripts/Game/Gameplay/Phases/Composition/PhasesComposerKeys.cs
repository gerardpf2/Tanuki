namespace Game.Gameplay.Phases.Composition
{
    public static class PhasesComposerKeys
    {
        public static class Phase
        {
            public const string CameraTargetHighestPlayerPieceLockRowPhase = nameof(CameraTargetHighestPlayerPieceLockRowPhase);
            public const string DestroyNotAlivePiecesPhase = nameof(DestroyNotAlivePiecesPhase);
            public const string GoalsCompletedPhase = nameof(GoalsCompletedPhase);
            public const string GravityPhase = nameof(GravityPhase);
            public const string InstantiateInitialPiecesPhase = nameof(InstantiateInitialPiecesPhase);
            public const string InstantiatePlayerPiecePhase = nameof(InstantiatePlayerPiecePhase);
            public const string LineClearPhase = nameof(LineClearPhase);
            public const string LockPlayerPiecePhase = nameof(LockPlayerPiecePhase);
            public const string NoMovesLeftPhase = nameof(NoMovesLeftPhase);
            public const string SimulateInstantiatePlayerPiecePhase = nameof(SimulateInstantiatePlayerPiecePhase);
        }

        public static class PhaseContainer
        {
            public const string Initial = nameof(Initial);
            public const string Lock = nameof(Lock);
            public const string Move = nameof(Move);
        }
    }
}