using System;
using UnityEngine;

namespace Game.Gameplay.Impacts
{
    public sealed class HitSensor : MonoBehaviour, IHittable
    {
        public event Action<ImpactHitData> ImpactEvent;
        public void Impact(ImpactHitData impactHitData)
        {
            ImpactEvent?.Invoke(impactHitData);
        }
    }
}