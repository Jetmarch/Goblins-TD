using System;
using UnityEngine;

namespace Game.Gameplay.Projectiles
{
    [Serializable]
    public class ProjectileData
    {
        public string ProjectileName => _projectileName;
        public bool IsPiercingThrough => _isPiercingThrough;
        public float Distance => _distance;
        
        [SerializeField] private string _projectileName;
        [SerializeField] private bool _isPiercingThrough;
        [SerializeField] private float _distance;

        public ProjectileData(ProjectileData root)
        {
            _projectileName = root.ProjectileName;
            _isPiercingThrough = root.IsPiercingThrough;
            _distance = root.Distance;
        }
    }
}