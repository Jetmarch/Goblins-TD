namespace Game.Gameplay.Impacts
{
    public interface IHittable
    {
        void Impact(ImpactHitData impactHitData);
    }

    public struct ImpactHitData
    {
        public float Damage;
        public float Knockback;

        public ImpactHitData(float damage, float knockback)
        {
            Damage = damage;
            Knockback = knockback;
        }
    }
}