using UnityEngine;

namespace Game.Gameplay.Weapons
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Gameplay/Weapons/WeaponConfig")]
    public sealed class WeaponConfig : ScriptableObject
    {
        [SerializeField] private WeaponData _weaponData;

        public WeaponData GetPrototype()
        {
            return new WeaponData(_weaponData);
        }
    }
}