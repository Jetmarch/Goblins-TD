using System;

namespace Game.Gameplay.WaveSystem
{
    public class WaveCounter
    {
        public event Action WaveDurationUpdated;
        public float CurrentWaveDuration => _currentWaveDuration;
        public float WaveDuration => _waveDuration;
        public event Action OnCounterComplete;
        private float _waveDuration;
        private float _currentWaveDuration;

        private bool _isActive = false;

        public void StartCounter(float waveDuration)
        {
            _waveDuration = waveDuration;
            _currentWaveDuration = 0f;
            _isActive = true;
        }
        
        public void UpdateCounter(float deltaTime)
        {
            if (!_isActive) return;
            
            _currentWaveDuration += deltaTime;
            WaveDurationUpdated?.Invoke();

            if (_currentWaveDuration >= _waveDuration)
            {
                OnCounterComplete?.Invoke();
            }
        }
    }
}