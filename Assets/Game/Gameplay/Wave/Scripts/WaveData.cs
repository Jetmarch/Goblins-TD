using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.WaveSystem
{
    [Serializable]
    public class WaveData
    {
        public float WaveDuration => _waveDuration;
        public float SpawnInterval => _spawnInterval;
        public IReadOnlyCollection<EnemySpawnData> EnemySpawnData => _enemySpawnData;
        
        [SerializeField] private float _waveDuration;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private List<EnemySpawnData> _enemySpawnData;
    }

    [Serializable]
    public struct EnemySpawnData
    {
        public string EnemyId;
        public int Count;
    }

    public enum EnemyType
    {
        FlashCrawler,
    }
}