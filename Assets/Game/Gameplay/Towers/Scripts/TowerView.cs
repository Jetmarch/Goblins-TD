using Game.Gameplay.Weapons;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Towers
{
    public class TowerView : MonoBehaviour, ITowerView
    {
        
        public Transform TowerTransform => transform;
        public Transform WeaponTransform => _weaponView.WeaponTransform;
        
        private IWeaponView _weaponView;
        
        [Inject]
        private void Initialize(IWeaponView weaponView)
        {
            _weaponView = weaponView;
        }

        public void PlayAttackFX()
        {
            _weaponView.PlayAttackAnimation();
            //Local VFX
            //Local SFX
        }
    }
}