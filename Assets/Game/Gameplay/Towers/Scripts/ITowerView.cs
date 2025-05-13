using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public interface ITowerView
    {
        Transform TowerTransform { get; }
        Transform WeaponTransform { get; }

        public void PlayAttackFX();
    }
}