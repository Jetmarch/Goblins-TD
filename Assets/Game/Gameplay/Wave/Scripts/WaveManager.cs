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
        [SerializeField] private int _currentWave;
        private WaveCounter _waveCounter;


        private void Awake()
        {
            _waveCounter = new WaveCounter();
            _waveCounter.OnCounterComplete += OnWaveDurationPassed;
        }

        private void Update()
        {
            _waveCounter.UpdateCounter(Time.deltaTime);
        }

        public void NextWave()
        {
            if (_currentWave >= _config.Waves.Count)
            {
                OnAllWavesComplete?.Invoke();
                return;
            }
            var nextWave = _config.Waves[_currentWave];
            _waveSpawner.AddWaveToSpawn(nextWave);
            _waveCounter.StartCounter(nextWave.WaveDuration);
            _currentWave++;
            OnStartWave?.Invoke();
        }

        private void OnWaveDurationPassed()
        {
            NextWave();
        }
    }
}