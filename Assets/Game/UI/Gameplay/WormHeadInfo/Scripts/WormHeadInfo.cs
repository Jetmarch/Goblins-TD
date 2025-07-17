using System;
using System.Collections.Generic;
using Game.Gameplay.Worm;
using UnityEngine;

namespace Game.UI.WormHeadInfo
{
    public sealed class WormHeadInfo : MonoBehaviour
    {
        [SerializeField] private GameObject _fireDamageTypePrefab;
        [SerializeField] private GameObject _iceDamageTypePrefab;
        [SerializeField] private GameObject _boltDamageTypePrefab;
        [SerializeField] private GameObject _punchDamageTypePrefab;

        [SerializeField] private Transform _damageTypeContainer;
        
        [SerializeField] private Worm _worm;


        [SerializeField] private List<GameObject> _currentDamageTypes;

        private void Start()
        {
            ClearDamageTypes();
            FillDamageTypes();
            _worm.HeadChanged += UpdateDamageTypes;
            _worm.HeadSuccessDamage += UpdateDamageTypes;
        }

        private void ClearDamageTypes()
        {
            foreach (var damageType in _currentDamageTypes)
            {
                Destroy(damageType.gameObject);
            }
        }

        private void FillDamageTypes()
        {
            var wormHead = _worm.Head;

            foreach (var damageType in wormHead.RemainingDamageTypesToDestroy)
            {
                var newDamageType = CreateDamageType(damageType);
                _currentDamageTypes.Add(newDamageType);
            }
        }

        private void UpdateDamageTypes()
        {
            ClearDamageTypes();
            FillDamageTypes();
        }


        private GameObject CreateDamageType(DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Fire:
                    return Instantiate(_fireDamageTypePrefab, _damageTypeContainer);
                case DamageType.Ice:
                    return  Instantiate(_iceDamageTypePrefab, _damageTypeContainer);
                case DamageType.Bolt:
                    return Instantiate(_boltDamageTypePrefab, _damageTypeContainer);
                case DamageType.Punch:
                    return Instantiate(_punchDamageTypePrefab, _damageTypeContainer);
                default:
                    return default;
            }
        }
    }
}