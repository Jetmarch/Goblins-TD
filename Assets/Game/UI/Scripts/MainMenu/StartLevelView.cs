using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class StartLevelView : MonoBehaviour
    {
        [SerializeField] private string _levelName;
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(LoadLevel);
        }

        private void LoadLevel()
        {
            var asyncOperation = SceneManager.LoadSceneAsync(_levelName, LoadSceneMode.Single);
            if (asyncOperation != null)
            {
                asyncOperation.completed += OnSceneLoaded;
            }
            else
            {
                throw new UnityException("Scene not found: " + _levelName);
            }
        }

        private void OnSceneLoaded(AsyncOperation obj)
        {
            
        }
    }
}