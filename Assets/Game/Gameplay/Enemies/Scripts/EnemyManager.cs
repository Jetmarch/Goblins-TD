using System;
using System.Collections.Generic;
using Game.GameEngine;
using Game.GameEngine.Common;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    [Prototype]
    public sealed class EnemyManager : MonoBehaviour, IEnemyManager
    {
        public bool IsAllEnemiesKilled => _activeEnemies.Count == 0;
        public event Action AllEnemiesKilled;
        
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private List<GameObject> _activeEnemies;
        
        public void CreateEnemy(string enemyId)
        {
            var enemy = _enemySpawner.SpawnEnemy(enemyId);
            _activeEnemies.Add(enemy);
            
            var enemyComponent = enemy.GetComponent<BasicEnemy>();
            enemyComponent.Died += DestroyEnemy;
        }

        private void DestroyEnemy(GameObject enemy)
        {
            _activeEnemies.Remove(enemy);
            
            Destroy(enemy);
            
            if (IsAllEnemiesKilled)
            {
                AllEnemiesKilled?.Invoke();
            }
        }
    }
    
    

    public interface IEnemyManager
    {
        bool IsAllEnemiesKilled { get; }
        event Action AllEnemiesKilled;
        void CreateEnemy(string enemyId);
    }
}