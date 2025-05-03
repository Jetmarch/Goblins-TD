using System;
using Game.GameEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Gameplay
{
    public class WaveView : MonoBehaviour
    {
        [SerializeField] private Button _startNextWaveButton;
        [SerializeField] private EnemySpawner _spawner;
        private void Start()
        {
            if (_startNextWaveButton == null)
            {
                throw new NullReferenceException("_startNextWaveButton is not set");
            }
            
            _startNextWaveButton.onClick.AddListener(StartNextWave);
        }

        private void StartNextWave()
        {
            _spawner.SetSpawningState(true);
        }
    }
}