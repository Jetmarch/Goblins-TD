using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.GameEngine
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _spawnRadius = 2f;
        [SerializeField] private Transform _spawnRoot;
        [SerializeField] private float _spawnInterval = 0.6f;

        [SerializeField] private List<GameObject> _spawnedEnemies;
        [SerializeField] private int _maxEnemies = 32;

        private void Start()
        {
            _spawnedEnemies = new List<GameObject>();
            StartCoroutine(SpawnEnemyRoutine());
        }

        private IEnumerator SpawnEnemyRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnInterval);
                ClearDestroyedEnemies();
                if (_spawnedEnemies.Count >= _maxEnemies)
                {
                    yield return new WaitForSeconds(_spawnInterval);
                }
                var randomSpawnPoint = (Vector2)_spawnPoint.position + (Random.insideUnitCircle * _spawnRadius);
                var newEnemy = Instantiate(_enemyPrefab, randomSpawnPoint, _enemyPrefab.transform.rotation, _spawnRoot);
                _spawnedEnemies.Add(newEnemy);
            }
        }

        private void ClearDestroyedEnemies()
        {
            for(int i = 0; i < _spawnedEnemies.Count; i++)
            {
                if (!_spawnedEnemies[i]) _spawnedEnemies.RemoveAt(i);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_spawnPoint.position, _spawnRadius);
        }
    }
}