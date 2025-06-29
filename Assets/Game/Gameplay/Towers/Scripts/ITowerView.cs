using UnityEngine;

namespace Game.Gameplay.Towers
{
    public interface ITowerView
    {
        Transform TowerTransform { get; }
        Transform WeaponTransform { get; }

        public void PlayAttackFX();
        public void SetAmmoAmountText(string text);
    }
}