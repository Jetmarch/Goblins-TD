using Game.Gameplay.Impacts;
using UnityEngine;

namespace Game.Gameplay.Projectiles
{
    public class ProjectileView : MonoBehaviour, IProjectileView
    {
        public Transform Transform => transform;
        public ImpactHitData ImpactHitData => _impactHitData;
        [SerializeField] private ParticleSystem _hitParticles;
        private ImpactHitData _impactHitData;

        public void PlayHitFX()
        {
            _hitParticles?.Play();
        }

        public void SetImpactData(ImpactHitData impactHitData)
        {
            _impactHitData = impactHitData;
        }
    }

    public interface IProjectileView
    {
        Transform Transform { get; }
        void PlayHitFX();
        
        ImpactHitData ImpactHitData { get; }
        void SetImpactData(ImpactHitData impactHitData);
    }
}