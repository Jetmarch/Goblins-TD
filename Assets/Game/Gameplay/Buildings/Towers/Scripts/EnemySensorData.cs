using System;

namespace Game.Gameplay.Buildings
{
    [Serializable]
    public class EnemySensorData
    {
        // ReSharper disable once InconsistentNaming
        public float Radius;

        public EnemySensorData(float radius)
        {
            Radius = radius;
        }
    }
}