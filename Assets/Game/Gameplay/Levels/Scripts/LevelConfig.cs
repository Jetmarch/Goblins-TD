using Game.Gameplay.LevelGrid;
using Game.Gameplay.WaveSystem;
using UnityEngine;

namespace Game.Gameplay.Levels
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Gameplay/Levels/LevelConfig")]
    public sealed class LevelConfig : ScriptableObject
    {
        public string LevelName => _levelName;
        public WaveDataConfig WaveDataConfig => _waveDataConfig;
        public LevelGridConfig LevelGridConfig => _levelGridConfig;
        
        [SerializeField] private string _levelName;
        [SerializeField] private WaveDataConfig _waveDataConfig;
        [SerializeField] private LevelGridConfig _levelGridConfig;
    }
}