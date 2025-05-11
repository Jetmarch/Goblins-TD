namespace Game.Gameplay.Weapons
{
    public class WeaponData
    {
        public readonly string ProjectileId;
        public readonly float AttackSpeed;

        public WeaponData(string projectileId, float attackSpeed)
        {
            ProjectileId = projectileId;
            AttackSpeed = attackSpeed;
        }
    }
}