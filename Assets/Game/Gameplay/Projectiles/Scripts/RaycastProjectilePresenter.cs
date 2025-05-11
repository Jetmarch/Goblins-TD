using Game.Gameplay.Impacts;
using UnityEngine;
using VContainer.Unity;

namespace Game.Gameplay.Projectiles
{
    internal sealed class RaycastProjectilePresenter : IProjectilePresenter, IStartable
    {
        private readonly IProjectileView _view;
        private readonly ProjectileData _projectileData;

        public RaycastProjectilePresenter(IProjectileView view, ProjectileData projectileData)
        {
            _view = view;
            _projectileData = projectileData;
        }


        public void Start()
        {
            var hit = Physics2D.Raycast(_view.Transform.position, _view.Transform.up, _projectileData.Distance, _projectileData.LayerMask);
            if (!hit.collider) return;
            
            Debug.Log($"Hitted some object with name {hit.collider.gameObject.name}");
            _view.PlayHitFX();
            if (!hit.collider.gameObject.TryGetComponent(out IHittable hittable)) return;
            
            hittable.Impact(_view.ImpactHitData);
        }
    }
}