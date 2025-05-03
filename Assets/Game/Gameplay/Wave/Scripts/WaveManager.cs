using System;
using UnityEngine;

namespace Game.Gameplay.WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        public event Action OnAllWavesComplete;
        public event Action OnStartWave;
        public int CurrentWave => _currentWave;
        
        [SerializeField] private WaveDataConfig _config;
        [SerializeField] private WaveSpawner _waveSpawner;

        private int _currentWave;
        
        public void NextWave()
        {
            _currentWave++;
            if (_currentWave >= _config.Waves.Count)
            {
                OnAllWavesComplete?.Invoke();
                return;
            }
            var nextWave = _config.Waves[_currentWave];
            _waveSpawner.AddWaveToSpawn(nextWave);
            OnStartWave?.Invoke();
        }
    }
}