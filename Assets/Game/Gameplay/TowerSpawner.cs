using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class TowerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _towerPrefab;
        [SerializeField] private Transform _spawnRoot;
        [SerializeField] private List<GameObject> _spawnedTowers;
        [SerializeField] private int _maxTowers = 64;
        
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _spawnedTowers = new List<GameObject>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SpawnTower();
            }
        }

        private void SpawnTower()
        {
            if (!CanSpawnTower()) return;
            
            var spawnPoint = GetSpawnPoint();
            var tower = Instantiate(_towerPrefab, spawnPoint, _towerPrefab.transform.rotation, _spawnRoot);
            _spawnedTowers.Add(tower);
        }

        private bool CanSpawnTower()
        {
            return _spawnedTowers.Count < _maxTowers;
        }

        private Vector2 GetSpawnPoint()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}