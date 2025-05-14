using UnityEngine;

namespace Game.Gameplay.Towers
{
    public class BasicTower : MonoBehaviour
    {
        [SerializeField] private float _attackRange = 2f;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _attackDelay = 0.5f;

        [SerializeField] private float _currentAttackDelay;
        private Collider2D[] _targets;
        private readonly int _maxTargets = 32;
        private ContactFilter2D _contactFilter;

        private BasicEnemy _currentTarget;
        private void Start()
        {
            _targets = new Collider2D[_maxTargets];
            _contactFilter = new ContactFilter2D();
        }

        private void Update()
        {
            if (IsTargetValid())
            {
                TryToAttackTarget();
                CheckDistanceToTarget();
            }
            else
            {
                ScanForNewTargets();
            }

            UpdateAttackDelay();
        }

        private bool IsTargetValid()
        {
            return _currentTarget;
        }

        private void TryToAttackTarget()
        {
            if (!(_currentAttackDelay >= _attackDelay)) return;
            
            _currentTarget.TakeDamage(_damage);
            _currentAttackDelay = 0f;
        }

        private void CheckDistanceToTarget()
        {
            if (Vector2.Distance(transform.position, _currentTarget.transform.position) > _attackRange)
            {
                _currentTarget = null;
            }
        }

        private void ScanForNewTargets()
        {
            //Used ContactFilter2D.NoFilter for checking collsion results
            var countOfScannedTargets = Physics2D.OverlapCircle(transform.position, _attackRange, _contactFilter.NoFilter(), _targets);
            if (countOfScannedTargets <= 0) return;
            if (!_currentTarget)
            {
                _currentTarget = _targets[0].gameObject.GetComponent<BasicEnemy>();
            }
        }

        private void UpdateAttackDelay()
        {
            if (_currentAttackDelay < _attackDelay)
            {
                _currentAttackDelay += Time.deltaTime;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 1, 0f, 0.2f);
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }

    }
}