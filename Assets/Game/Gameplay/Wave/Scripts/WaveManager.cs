using System;
using Game.GameEngine.Common;
using UnityEngine;

namespace Game.Gameplay.WaveSystem
{
    [Prototype]
    public class WaveManager : MonoBehaviour, IWaveManager
    {
        public event Action AllWavesComplete;
        public event Action OnStartWave;
        public int CurrentWave => _currentWave;
        
        [SerializeField] private WaveDataConfig _config;
        [SerializeField] private WaveSpawner _waveSpawner;
        [SerializeField] private int _currentWave;
        private WaveCounter _waveCounter;
        
        private bool _wavesComplete;


        private void Awake()
        {
            _wavesComplete = false;
            _waveCounter = new WaveCounter();
            _waveCounter.OnCounterComplete += OnWaveDurationPassed;
        }

        private void Update()
        {
            if (_wavesComplete) return;
            
            _waveCounter.UpdateCounter(Time.deltaTime);
        }

        public void NextWave()
        {
            if (_currentWave >= _config.Waves.Count)
            {
                _wavesComplete = true;
                AllWavesComplete?.Invoke();
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

    public interface IWaveManager
    {
        event Action AllWavesComplete;
    }
}