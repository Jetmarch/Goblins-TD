using System.Collections.Generic;
using Game.App;
using Game.GameEngine.Common;
using UnityEngine;

namespace Game.Gameplay.Levels
{
    [Prototype]
    public sealed class LevelManager : MonoSingleton<LevelManager>
    {
        public static List<LevelConfig> Levels => Instance._levels;
        
        [SerializeField] private List<LevelConfig> _levels;
        [SerializeField] private string _gameplaySceneName;

        private LevelConfig _selectedLevel;
        
        public static void LoadLevel(string levelName)
        {
            Instance._selectedLevel = Instance._levels.Find(level => level.LevelName == levelName);
            LoadingManager.LoadScene(Instance._gameplaySceneName);
        }

        public static LevelConfig GetCurrentLevel()
        {
            return Instance._selectedLevel;
        }
    }

    // public interface ILevelManager
    // {
    //     List<LevelConfig> Levels { get; }
    //     void LoadLevel(string levelName);
    // }
}