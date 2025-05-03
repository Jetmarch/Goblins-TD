using System;
using Game.GameEngine;
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
        [SerializeField] private WaveManager _waveManager;
        private void Start()
        {
            if (_startNextWaveButton == null)
            {
                throw new NullReferenceException("_startNextWaveButton is not set");
            }
            
            _startNextWaveButton.onClick.AddListener(StartNextWave);

            _waveManager.OnStartWave += SetWaveCounter;
            
            SetWaveCounter();
        }

        private void StartNextWave()
        {
            _waveManager.NextWave();
        }

        private void SetWaveCounter()
        {
            _waveCounter.text = _waveManager.CurrentWave.ToString();
        }
    }
}