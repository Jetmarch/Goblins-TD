using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameEngine.EntityComponents
{
    public sealed class ComponentStorage : MonoBehaviour, IComponentStorage
    {
        private readonly Dictionary<int, IComponent> _components = new(); 

        public IComponent Get(int componentId) 
        {
            return _components.GetValueOrDefault(componentId);
        }

        public void Set(int componentId, IComponent component)
        {
            if (!_components.TryAdd(componentId, component ?? throw new ArgumentNullException()))
            {
                Debug.LogWarning($"Component with id {componentId} already exists in the storage of {gameObject.name}");
            }
        }
    }
}