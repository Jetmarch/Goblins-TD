using Game.Gameplay.Enemies;
using Game.Gameplay.WaveSystem;

namespace Game.Gameplay.Conditions
{
    public static class VictoryConditionsUseCases
    {
        public static bool IsVictory(IEnemyManager enemyManager)
        {
            return enemyManager.IsAllEnemiesKilled;
        }
    }
}