using Game.Gameplay.Towers;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class DummyEnemy : MonoBehaviour, ITarget
    {
        public Transform Transform => transform;
    }
}