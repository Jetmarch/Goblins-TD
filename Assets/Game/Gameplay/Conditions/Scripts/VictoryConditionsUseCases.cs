using Game.Gameplay.Enemies;
using Game.Gameplay.WaveSystem;

namespace Game.Gameplay.Conditions
{
    public static class VictoryConditionsUseCases
    {
        public static bool IsVictory(IWaveManager waveManager, IEnemyManager enemyManager)
        {
            return waveManager.IsAllWavesComplete && enemyManager.IsAllEnemiesKilled;
        }
    }
}