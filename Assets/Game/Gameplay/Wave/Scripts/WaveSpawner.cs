using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.GameEngine.Common;
using Game.Gameplay.Enemies;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.WaveSystem
{
    [Prototype]
    public sealed class WaveSpawner : MonoBehaviour
    {
        public event Action WaveDataEmpty;
        
        private IEnemyManager _enemyManager;
        private List<WaveData> _wavesToSpawn;

        [Inject]
        private void Configure(IEnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
        }

        private void Awake()
        {
            _wavesToSpawn = new List<WaveData>();
        }

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                if (_wavesToSpawn.Count == 0)
                {
                    WaveDataEmpty?.Invoke();
                }
                
                yield return new WaitUntil(() => _wavesToSpawn.Count > 0);
                for (int i = 0; i < _wavesToSpawn.Count; i++)
                {
                    var wave = _wavesToSpawn[i];
                    for (int k = 0; k < wave.EnemySpawnData.Count; k++)
                    {
                        var enemySpawnData = wave.EnemySpawnData.ElementAt(k);
                        for (int j = 0; j < enemySpawnData.Count; j++)
                        {
                            _enemyManager.CreateEnemy(enemySpawnData.EnemyId);
                            yield return new WaitForSeconds(wave.SpawnInterval);
                        }
                    }
                    _wavesToSpawn.RemoveAt(i);
                }
            }
        }
        
        public void AddWaveToSpawn(WaveData waveData)
        {
            _wavesToSpawn.Add(waveData);
        }
    }
}