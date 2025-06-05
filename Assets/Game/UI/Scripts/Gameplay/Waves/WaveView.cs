using System;
using Game.Gameplay.WaveSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.UI.Gameplay
{
    public class WaveView : MonoBehaviour
    {
        [SerializeField] private Button _startNextWaveButton;
        [SerializeField] private TextMeshProUGUI _waveCounter;
        [SerializeField] private Slider _waveDurationSlider;
        [SerializeField] private WaveManager _waveManager;
        private void Start()
        {
            if (_startNextWaveButton == null)
            {
                throw new NullReferenceException("_startNextWaveButton is not set");
            }
            
            _startNextWaveButton.onClick.AddListener(StartNextWave);

            _waveManager.StartWave += SetWaveInfo;
            _waveManager.AllWavesComplete += SetFinalWaveText;
            _waveManager.WaveCounter.WaveDurationUpdated += SetWaveDuration;
            _waveDurationSlider.value = 1;
            
            SetWaveInfo();
        }

        private void StartNextWave()
        {
            _waveManager.NextWave();
        }

        private void SetWaveInfo()
        {
            _waveCounter.text = $"Wave: {_waveManager.CurrentWave}";
            _waveDurationSlider.value = 1;
        }

        private void SetFinalWaveText()
        {
            _waveCounter.text = "Final wave!";
        }

        private void SetWaveDuration()
        {
            _waveDurationSlider.value = 1 - (_waveManager.CurrentWaveDuration / _waveManager.WaveDuration);
        }
    }
}