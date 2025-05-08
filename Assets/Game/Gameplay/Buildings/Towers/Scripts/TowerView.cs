using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private GameObject _weapon;
        [SerializeField] private GameObject _projectilePrefab;
        
        [SerializeField] private TowerConfig _config;

        [SerializeField] private EnemySensor _enemySensor;

        [SerializeField] private GameObject _currentTarget;
        private TowerModel _towerModel;
        
        private Rotator _rotator;

        private void Start()
        {
            _towerModel = _config.GetPrototype();

            _enemySensor.OnEnemyDetected += SetTarget;
            _enemySensor.OnEnemyLost += (x) => SetTarget(null);
            _enemySensor.SetRadius(_towerModel.Range);
        }

        private void Update()
        {
            if (!_currentTarget) return;
            
            _rotator.RotateTowardsTarget(_currentTarget.transform.position, _weapon.transform, _towerModel.RotateSpeed, Time.deltaTime);
        }

        public void SetTarget(GameObject target)
        {
            _currentTarget = target;
        }
    }
}