using System;

namespace Game.Gameplay.Buildings
{
    public interface ITargetSensor
    {
        event Action<ITarget> EnemyDetected;
        event Action<ITarget> EnemyLost;
    }
}