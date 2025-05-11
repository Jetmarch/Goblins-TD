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

        public void SpawnEnemy(string enemyId)
        {
            //TODO: Get enemy prefab by id
            //Spawn it
            var spawnPosition = GetRandomSpawnPosition();
            Instantiate(_enemyPrefab, spawnPosition, _enemyPrefab.transform.rotation, _spawnRoot);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            return (Vector2)_spawnPoint.position + (Random.insideUnitCircle * _spawnRadius);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_spawnPoint.position, _spawnRadius);
        }
    }
}