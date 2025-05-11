using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class WeaponView : MonoBehaviour, IWeaponView
    {
        public Transform WeaponTransform => transform;
        public Transform ShootPointTransform => _shootPoint;

        [SerializeField] private Animator _weaponAnimator;
        [SerializeField] private string _attackAnimationName = "Attack";
        [SerializeField] private Transform _shootPoint;

        private int _attackTrigger;

        private void Awake()
        {
            _attackTrigger = Animator.StringToHash(_attackAnimationName);
        }
        
        public void PlayAttackAnimation()
        {
            _weaponAnimator.SetTrigger(_attackTrigger);
        }
    }

    public interface IWeaponView
    {
        Transform ShootPointTransform { get; }
        Transform WeaponTransform { get; }
        void PlayAttackAnimation();
    }
}