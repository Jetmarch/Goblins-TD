using UnityEngine;

namespace Game.Gameplay.LevelGrid
{
    [CreateAssetMenu(fileName = "LevelGridConfig", menuName = "Gameplay/Levels/LevelGridConfig")]
    public sealed class LevelGridConfig : ScriptableObject
    {
        public LevelGrid LevelGrid => _levelGrid;
        [SerializeField] private LevelGrid _levelGrid;
        
        
        public void SetLevelGrid(LevelGrid levelGrid)
        {
            _levelGrid = levelGrid;
        }

        public LevelGrid GetPrototype()
        {
            return new LevelGrid(_levelGrid);
        }
    }
}