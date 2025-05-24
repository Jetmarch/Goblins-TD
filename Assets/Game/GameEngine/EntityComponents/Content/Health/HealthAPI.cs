using System;

namespace Game.GameEngine.EntityComponents
{
    public static class HealthAPI
    {
        private const int HealthComponentId = 1;

        public static HealthStorage GetHealthStorage(this IComponentStorage componentStorage)
        {
            if (componentStorage.Get(HealthComponentId) is HealthStorage healthStorage)
            {
                return healthStorage;
            }
            
            throw new ApplicationException($"Health storage not found");
        }
        
        public static void AddHealthStorage(this IComponentStorage componentStorage, HealthStorage healthStorage)
        {
            componentStorage.Set(HealthComponentId, healthStorage);
        }
    }
}