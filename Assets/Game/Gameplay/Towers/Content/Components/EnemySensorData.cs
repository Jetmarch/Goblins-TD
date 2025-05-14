using System;

namespace Game.Gameplay.Towers.Components
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