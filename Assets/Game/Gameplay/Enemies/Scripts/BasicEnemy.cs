using System;
using System.Collections.Generic;
using System.Linq;
using Game.GameEngine;
using Game.GameEngine.GridSystem;
using Game.GameEngine.Pathfinding;
using Game.UI;
using UnityEngine;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicEnemy : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private HealthStorage _healthStorage;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [SerializeField] private Bounds _deathBounds;
        
        [SerializeField] private EntityHealthBarView _healthBarView;
        
        [SerializeField] private GridManager _gridManager;

        [SerializeField] private PlayerBase _playerBase;
        
        [SerializeField] private int _currentPathIndex;

        private List<ICell> _path;
        private Pathfinder _pathfinder;

        private ICell _startPoint;
        private ICell _endPoint;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _healthStorage.Reset();
            _path = new List<ICell>();
            
            _gridManager = GameObject.Find("TowerGrid").GetComponent<GridManager>();
            _playerBase = FindFirstObjectByType<PlayerBase>();

            _pathfinder = new Pathfinder(new AStarPathfinding());
            var startPoint = _gridManager.Grid.GetCellByWorldPositionOrDefault(transform.position);
            if (startPoint == default)
            {
                throw new ApplicationException("Enemy is not in pathfinding grid");
            }
            
            var endPoint = _gridManager.Grid.GetCellByWorldPositionOrDefault(_playerBase.transform.position);
            if (endPoint == default)
            {
                throw new ApplicationException("Player base is not in pathfinding grid");
            }

            if (!_pathfinder.FindPath(startPoint, endPoint, _gridManager.Grid, out _path))
            {
                throw new ApplicationException($"Path not found for {gameObject.name}");
            }

            _currentPathIndex = _path.Count - 1;
            
        }

        private void OnEnable()
        {
            _healthStorage.OnDeath += Die;
            _healthStorage.OnHealthChanged += UpdateHealthBar;
        }

        private void OnDisable()
        {
            _healthStorage.OnDeath -= Die;
            _healthStorage.OnHealthChanged -= UpdateHealthBar;
        }

        private void FixedUpdate()
        {
            var pathPoint = _path.ElementAt(_currentPathIndex);
            
            var desiredPosition = new Vector2(pathPoint.WorldX, pathPoint.WorldY);
            var currentPosition = _rigidbody.position;
            
            Debug.Log($"Desired position: {desiredPosition}");
            
            
            var direction = desiredPosition - currentPosition;
            
            _rigidbody.MovePosition(_rigidbody.position + direction * (_speed * Time.fixedDeltaTime));
            if (Vector2.Distance(desiredPosition, currentPosition) <= 0.1f)
            {
                _currentPathIndex--;
            }
        }

        public void TakeDamage(float damage)
        {
            _healthStorage.DecreaseHealth(damage);
            
        }

        private void UpdateHealthBar()
        {
            _healthBarView.UpdateHealthBar(_healthStorage.CurrentHealth, _healthStorage.MaxHealth);
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_deathBounds.center, _deathBounds.size);

            Gizmos.color = Color.blue;

            var cellVectors = new Vector3[_path.Count];
            for(int i=0; i<_path.Count; i++)
            {
                var cellVector = new Vector3(_path[i].WorldX * _path[i].Size + _gridManager.Grid.WorldPosition.x, _path[i].WorldY * _path[i].Size + _gridManager.Grid.WorldPosition.y, 0);
                cellVectors[i] = cellVector;
            }

            Gizmos.DrawLineStrip(cellVectors, false);
        }
    }
}