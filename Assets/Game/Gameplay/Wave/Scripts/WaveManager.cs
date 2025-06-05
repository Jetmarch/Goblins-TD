using System;
using Game.GameEngine.Common;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.WaveSystem
{
    [Prototype]
    public sealed class WaveManager : MonoBehaviour, IWaveManager
    {
        public event Action AllWavesComplete;
        public WaveCounter WaveCounter => _waveCounter;
        public bool IsAllWavesComplete => _currentWave >= _waveDataConfig.Waves.Count;
        public event Action StartWave;
        public int CurrentWave => _currentWave;
        public float CurrentWaveDuration => _waveCounter.CurrentWaveDuration;
        public float WaveDuration => _waveCounter.WaveDuration;
        
        [SerializeField] private WaveDataConfig _waveDataConfig;
        [SerializeField] private WaveSpawner _waveSpawner;
        [SerializeField] private int _currentWave;
        private WaveCounter _waveCounter;
        
        private bool _wavesComplete;

        [Inject]
        private void Configure(WaveSpawner waveSpawner, WaveDataConfig waveDataConfig)
        {
            _waveSpawner = waveSpawner;
            _waveDataConfig = waveDataConfig;
        }

        private void Awake()
        {
            _wavesComplete = false;
            _waveCounter = new WaveCounter();
            _waveCounter.OnCounterComplete += OnWaveDurationPassed;
        }

        private void OnEnable()
        {
            _waveSpawner.WaveDataEmpty += WaveSpawnerDataEmpty;
        }

        private void OnDisable()
        {
            _waveSpawner.WaveDataEmpty -= WaveSpawnerDataEmpty;
        }

        private void WaveSpawnerDataEmpty()
        {
            _wavesComplete = IsAllWavesComplete;
            if (!_wavesComplete) return;
            
            AllWavesComplete?.Invoke();
        }

        private void Update()
        {
            if (_wavesComplete) return;
            
            _waveCounter.UpdateCounter(Time.deltaTime);
            
        }

        public void NextWave()
        {
            _wavesComplete = IsAllWavesComplete;
            if (_wavesComplete)
            {
                AllWavesComplete?.Invoke();
                return;
            }
            
            var nextWave = _waveDataConfig.Waves[_currentWave];
            _waveSpawner.AddWaveToSpawn(nextWave);
            _waveCounter.StartCounter(nextWave.WaveDuration);
            _currentWave++;
            StartWave?.Invoke();
        }

        private void OnWaveDurationPassed()
        {
            NextWave();
        }
    }

    public interface IWaveManager
    {
        event Action AllWavesComplete;
        bool IsAllWavesComplete { get; }
    }
}