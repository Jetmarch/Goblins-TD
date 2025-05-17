
using System.Reflection;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Game.GameEngine.Common
{
    public static class PrototypeAttributeChecker 
    {
#if UNITY_EDITOR
        [DidReloadScripts]
        public static void OnPostProcessBuild()
        {
            CheckForPrototypes();
        }
        private static void CheckForPrototypes()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var attributes = type.GetCustomAttributes(typeof(PrototypeAttribute), false);
                if (attributes.Length > 0)
                {
                    Debug.LogWarning($"Warning: {type.Name} is marked with PrototypeAttribute. Message: {((PrototypeAttribute)attributes[0]).Message}");
                }
            }
        }
#endif
    }
}