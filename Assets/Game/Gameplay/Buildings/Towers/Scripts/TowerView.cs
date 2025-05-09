using System;
using Game.Gameplay.Weapons;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Buildings
{
    public class TowerView : MonoBehaviour
    {
        //TODO:
        public event Action AttackRequest;
        
        [SerializeField] private GameObject _currentTarget;
        
        private WeaponView _weaponView;
        private TowerData _towerData;
        
        //VFX
        //SFX
        
        [Inject]
        private void Initialize(TowerData towerData, WeaponView weaponView)
        {
            _towerData = towerData;
            _weaponView = weaponView;
        }

        private void Update()
        {
            if (!_currentTarget) return;
            
            var deltaTime = Time.deltaTime;
            
            Rotator.RotateTowardsTarget(_currentTarget.transform.position, _weaponView.transform, _towerData.RotateSpeed, deltaTime);

            if (ConeDetector.IsTargetInCone(_currentTarget.transform.position, _weaponView.transform, _towerData.AttackAngle))
            {
                AttackRequest?.Invoke();
                
                _weaponView.Attack();
            }
        }

        public void SetTarget(GameObject target)
        {
            _currentTarget = target;
        }

        public void TargetLost(GameObject _)
        {
            _currentTarget = null;
        }
    }
}