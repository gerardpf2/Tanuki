using UnityEngine;

namespace Editor.Game.Gameplay.Editor
{
    [CreateAssetMenu(fileName = nameof(GameplayEditorParametersContainer), menuName = "Tanuki/Editor/Game/Gameplay/" + nameof(GameplayEditorParametersContainer))]
    public class GameplayEditorParametersContainer : ScriptableObject, IGameplayEditorParametersGetter
    {
        [SerializeField] private int _minColumns = 4;
        [SerializeField] private int _maxColumns = 9;
        [SerializeField] private int _initialColumns = 7;

        public int MinColumns => _minColumns;

        public int MaxColumns => _maxColumns;

        public int InitialColumns => _initialColumns;
    }
}