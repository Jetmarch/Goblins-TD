using System;

namespace Game.Gameplay.Towers
{
    public interface ITargetSensor
    {
        event Action<ITarget> EnemyDetected;
        event Action<ITarget> EnemyLost;
    }
}