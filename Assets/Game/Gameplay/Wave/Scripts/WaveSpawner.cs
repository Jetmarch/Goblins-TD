using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.GameEngine;
using UnityEngine;

namespace Game.Gameplay.WaveSystem
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        
        private List<WaveData> _wavesToSpawn;

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
                yield return new WaitUntil(() => _wavesToSpawn.Count > 0);
                for (int i = 0; i < _wavesToSpawn.Count; i++)
                {
                    var wave = _wavesToSpawn[i];
                    for (int k = 0; k < wave.EnemySpawnData.Count; k++)
                    {
                        var enemySpawnData = wave.EnemySpawnData.ElementAt(k);
                        for (int j = 0; j < enemySpawnData.Count; j++)
                        {
                            _enemySpawner.SpawnEnemy(enemySpawnData.EnemyId);
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