using System;
using System.Collections.Generic;
using System.Linq;
using Game.GameEngine;
using Game.GameEngine.Common;
using Game.GameEngine.Pathfinding;
using Game.Gameplay.Coins;
using Game.Gameplay.Impacts;
using Game.Gameplay.Levels;
using Game.Gameplay.Towers;
using Game.Gameplay.Towers.PlayerBase;
using Game.UI;
using UnityEngine;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    [Prototype]
    public sealed class BasicEnemy : MonoBehaviour, ITarget, IHittable
    {
        public Transform Transform => transform;
        
        [SerializeField] private float _speed = 1f;
        [SerializeField] private HealthStorage _healthStorage;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [SerializeField] private Bounds _deathBounds;
        
        [SerializeField] private EntityHealthBarView _healthBarView;
        
        [SerializeField] private GridManager _gridManager;

        [SerializeField] private PlayerBaseInstaller _playerBase;
        
        [SerializeField] private int _currentPathIndex;

        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private int _countOfMoneyDropOnDeath = 5;

        private List<ILevelCell> _path;
        private Pathfinder _pathfinder;

        private ILevelCell _startPoint;
        private ILevelCell _endPoint;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _healthStorage.Reset();
            _path = new List<ILevelCell>();
            
            _coinsView = GameObject.Find("CoinsView").GetComponent<CoinsView>();
            
            _gridManager = GameObject.Find("TowerGrid").GetComponent<GridManager>();
            _playerBase = FindFirstObjectByType<PlayerBaseInstaller>();

            _pathfinder = new Pathfinder(new AStarPathfinding());
            var startPoint = LevelGridUseCases.GetCellByWorldPositionOrDefault(_gridManager.Grid, transform.position);
            if (startPoint == default)
            {
                throw new ApplicationException("Enemy is not in pathfinding grid");
            }
            
            Debug.Log($"Start point is: {startPoint.GridX}, {startPoint.GridY}");
            
            var endPoint = LevelGridUseCases.GetCellByWorldPositionOrDefault(_gridManager.Grid, _playerBase.transform.position);
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
            if (_currentPathIndex <= 0)
            {
                Destroy(gameObject);
                return;
            }
            var pathPoint = _path.ElementAt(_currentPathIndex);
            
            var desiredPosition = new Vector2(pathPoint.WorldX, pathPoint.WorldY);
            var currentPosition = _rigidbody.position;
            
            
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
            _coinsView.AddCoins(_countOfMoneyDropOnDeath);
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            if(_path == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_deathBounds.center, _deathBounds.size);

            Gizmos.color = Color.blue;

            var cellVectors = new Vector3[_path.Count];
            for(int i=0; i<_path.Count; i++)
            {
                var cellVector = new Vector3(_path[i].WorldX + _gridManager.Grid.WorldPosition.x, _path[i].WorldY + _gridManager.Grid.WorldPosition.y, 0);
                cellVectors[i] = cellVector;
            }

            Gizmos.DrawLineStrip(cellVectors, false);
        }

        public void Impact(ImpactHitData impactHitData)
        {
            TakeDamage(impactHitData.Damage);
        }
    }
}