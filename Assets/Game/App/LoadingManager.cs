using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.App
{
    public class LoadingManager : MonoBehaviour
    {
        private static LoadingManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void LoadScene(string sceneName)
        {
            _instance.LoadSceneImpl(sceneName);
        }

        private void LoadSceneImpl(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            if (asyncOperation != null)
            {
                asyncOperation.completed += OnSceneLoaded;
            }
            else
            {
                throw new UnityException("Scene not found: " + sceneName);
            }
        }
        
        private void OnSceneLoaded(AsyncOperation obj)
        {
            
        }
    }
}